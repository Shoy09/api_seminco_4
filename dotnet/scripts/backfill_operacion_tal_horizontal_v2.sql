BEGIN;

CREATE TEMP TABLE tmp_otv2_map (
    legacy_id integer PRIMARY KEY,
    new_id integer NOT NULL
) ON COMMIT DROP;

INSERT INTO tmp_otv2_map (legacy_id, new_id)
SELECT external_sync_id::integer, id
FROM operacion_tal_horizontal_v2
WHERE external_sync_id ~ '^[0-9]+$'
ON CONFLICT (legacy_id) DO NOTHING;

WITH source_rows AS (
    SELECT
        o.id AS legacy_id,
        CASE
            WHEN o.fecha IS NULL OR btrim(o.fecha) = '' THEN now()
            WHEN o.fecha ~ '^\d{4}-\d{2}-\d{2}$' THEN ((o.fecha || ' 00:00:00')::timestamp AT TIME ZONE 'UTC')
            WHEN o.fecha ~ '^\d{4}-\d{2}-\d{2}\s+\d{2}:\d{2}(:\d{2})?$' THEN (o.fecha::timestamp AT TIME ZONE 'UTC')
            WHEN o.fecha ~ '^\d{2}/\d{2}/\d{4}$' THEN (to_timestamp(o.fecha, 'DD/MM/YYYY') AT TIME ZONE 'UTC')
            ELSE now()
        END AS fecha,
        o.turno,
        o.operador,
        o.jefe_guardia,
        o.equipo AS equipo_nombre,
        o.n_equipo,
        o.seccion,
        o.modelo_equipo,
        COALESCE(o.estado, 'activo') AS estado,
        COALESCE(o.envio, 0) AS envio,
        COALESCE(o.revisado, 0) AS revisado,
        COALESCE(o.aprobacion, 0) AS aprobacion,
        o.observaciones_jefe::text AS observaciones_jefe,
        o.observaciones_jefe2::text AS observaciones_jefe2,
        o.observaciones_jefe3::text AS observaciones_jefe3,
        jsonb_build_object(
            'registros', COALESCE(NULLIF(o.registros, ''), '[]')::jsonb,
            'horometros', COALESCE(NULLIF(o.horometros, ''), '{}')::jsonb,
            'condiciones_equipo', COALESCE(NULLIF(o.condiciones_equipo, ''), '{}')::jsonb,
            'check_list', COALESCE(NULLIF(o.check_list, ''), '[]')::jsonb,
            'control_llantas', COALESCE(NULLIF(o.control_llantas, ''), '{}')::jsonb
        )::jsonb AS payload_original,
        now() AS created_at,
        now() AS updated_at
    FROM "Operacion_tal_horizontal" o
    WHERE NOT EXISTS (
        SELECT 1
        FROM tmp_otv2_map m
        WHERE m.legacy_id = o.id
    )
), inserted AS (
    INSERT INTO operacion_tal_horizontal_v2 (
        fecha,
        turno,
        operador,
        jefe_guardia,
        equipo_nombre,
        n_equipo,
        seccion,
        modelo_equipo,
        estado,
        envio,
        revisado,
        aprobacion,
        observaciones_jefe,
        observaciones_jefe2,
        observaciones_jefe3,
        payload_original,
        payload_version,
        external_sync_id,
        created_at,
        updated_at
    )
    SELECT
        s.fecha,
        s.turno,
        s.operador,
        s.jefe_guardia,
        s.equipo_nombre,
        s.n_equipo,
        s.seccion,
        s.modelo_equipo,
        s.estado,
        s.envio,
        s.revisado,
        s.aprobacion,
        s.observaciones_jefe,
        s.observaciones_jefe2,
        s.observaciones_jefe3,
        s.payload_original::jsonb,
        'legacy-v1',
        s.legacy_id::text,
        s.created_at,
        s.updated_at
    FROM source_rows s
    RETURNING id, external_sync_id
)
INSERT INTO tmp_otv2_map (legacy_id, new_id)
SELECT external_sync_id::integer, id
FROM inserted
ON CONFLICT (legacy_id) DO NOTHING;

DELETE FROM operacion_tal_horizontal_checklist
WHERE operacion_id IN (SELECT new_id FROM tmp_otv2_map);

DELETE FROM operacion_tal_horizontal_control_llanta
WHERE operacion_id IN (SELECT new_id FROM tmp_otv2_map);

DELETE FROM operacion_tal_horizontal_horometro
WHERE operacion_id IN (SELECT new_id FROM tmp_otv2_map);

DELETE FROM operacion_tal_horizontal_condicion_equipo
WHERE operacion_id IN (SELECT new_id FROM tmp_otv2_map);

DELETE FROM operacion_tal_horizontal_registro_detalle
WHERE registro_id IN (
    SELECT id
    FROM operacion_tal_horizontal_registro
    WHERE operacion_id IN (SELECT new_id FROM tmp_otv2_map)
);

DELETE FROM operacion_tal_horizontal_registro
WHERE operacion_id IN (SELECT new_id FROM tmp_otv2_map);

INSERT INTO operacion_tal_horizontal_horometro (
    operacion_id,
    tipo,
    inicio,
    final,
    op,
    inop
)
SELECT
    m.new_id,
    h.tipo,
    NULLIF(h.payload->>'inicio', '')::numeric(10,2),
    NULLIF(h.payload->>'final', '')::numeric(10,2),
    COALESCE((h.payload->>'op')::boolean, false),
    COALESCE((h.payload->>'inop')::boolean, false)
FROM "Operacion_tal_horizontal" o
JOIN tmp_otv2_map m ON m.legacy_id = o.id
CROSS JOIN LATERAL (
    VALUES
        ('diesel', COALESCE(NULLIF(o.horometros, ''), '{}')::jsonb -> 'diesel'),
        ('electrico', COALESCE(NULLIF(o.horometros, ''), '{}')::jsonb -> 'electrico'),
        ('percusion', COALESCE(NULLIF(o.horometros, ''), '{}')::jsonb -> 'percusion')
) AS h(tipo, payload)
WHERE h.payload IS NOT NULL;

INSERT INTO operacion_tal_horizontal_condicion_equipo (
    operacion_id,
    op,
    no_op,
    lugar,
    descripcion,
    aceite_motor,
    aceite_hidraulico,
    aceite_transmision,
    combustible,
    hora_llenado
)
SELECT
    m.new_id,
    COALESCE((c.payload->>'op')::boolean, false),
    COALESCE((c.payload->>'noOp')::boolean, false),
    NULLIF(c.payload->>'lugar', ''),
    NULLIF(c.payload->>'descripcion', ''),
    COALESCE((c.payload->>'aceiteMotor')::boolean, false),
    COALESCE((c.payload->>'aceiteHidraulico')::boolean, false),
    COALESCE((c.payload->>'aceiteTransmision')::boolean, false),
    NULLIF(c.payload->>'combustible', ''),
    CASE
        WHEN NULLIF(c.payload->>'horaLlenado', '') ~ '^\d{1,2}:\d{2}(:\d{2})?$' THEN (c.payload->>'horaLlenado')::time
        ELSE NULL
    END
FROM "Operacion_tal_horizontal" o
JOIN tmp_otv2_map m ON m.legacy_id = o.id
CROSS JOIN LATERAL (
    SELECT COALESCE(NULLIF(o.condiciones_equipo, ''), '{}')::jsonb AS payload
) c
WHERE c.payload <> '{}'::jsonb;

INSERT INTO operacion_tal_horizontal_checklist (
    operacion_id,
    checklist_item_id,
    categoria_snapshot,
    descripcion_snapshot,
    decision,
    observacion
)
SELECT
    m.new_id,
    NULL,
    COALESCE(item->>'categoria', ''),
    COALESCE(item->>'descripcion', ''),
    COALESCE(NULLIF(item->>'decision', '')::integer, 0),
    NULLIF(item->>'observacion', '')
FROM "Operacion_tal_horizontal" o
JOIN tmp_otv2_map m ON m.legacy_id = o.id
CROSS JOIN LATERAL jsonb_array_elements(COALESCE(NULLIF(o.check_list, ''), '[]')::jsonb) item;

INSERT INTO operacion_tal_horizontal_control_llanta (
    operacion_id,
    posicion,
    estado,
    presion,
    observacion
)
SELECT
    m.new_id,
    x.posicion,
    x.estado,
    NULL,
    NULL
FROM "Operacion_tal_horizontal" o
JOIN tmp_otv2_map m ON m.legacy_id = o.id
CROSS JOIN LATERAL (
    VALUES
        (1::smallint, COALESCE((COALESCE(NULLIF(o.control_llantas, ''), '{}')::jsonb->>'numero1')::boolean, false)),
        (2::smallint, COALESCE((COALESCE(NULLIF(o.control_llantas, ''), '{}')::jsonb->>'numero2')::boolean, false)),
        (3::smallint, COALESCE((COALESCE(NULLIF(o.control_llantas, ''), '{}')::jsonb->>'numero3')::boolean, false)),
        (4::smallint, COALESCE((COALESCE(NULLIF(o.control_llantas, ''), '{}')::jsonb->>'numero4')::boolean, false))
) AS x(posicion, estado);

WITH registros_source AS (
    SELECT
        m.new_id AS operacion_id,
        t.item AS registro,
        t.ordinality AS source_ordinal,
        now() AS ts
    FROM "Operacion_tal_horizontal" o
    JOIN tmp_otv2_map m ON m.legacy_id = o.id
    CROSS JOIN LATERAL jsonb_array_elements(COALESCE(NULLIF(o.registros, ''), '[]')::jsonb) WITH ORDINALITY AS t(item, ordinality)
), registros_inserted AS (
    INSERT INTO operacion_tal_horizontal_registro (
        operacion_id,
        external_id,
        numero,
        estado_principal,
        codigo_estado,
        hora_inicio,
        hora_final,
        payload_operacion,
        created_at,
        updated_at
    )
    SELECT
        s.operacion_id,
        CASE WHEN COALESCE(s.registro->>'id', '') ~ '^[0-9]+$' THEN (s.registro->>'id')::bigint ELSE NULL END,
        COALESCE(NULLIF(s.registro->>'numero', '')::integer, 0),
        COALESCE(s.registro->>'estado', ''),
        COALESCE(s.registro->>'codigo', ''),
        CASE
            WHEN COALESCE(s.registro->>'hora_inicio', '') ~ '^\d{1,2}:\d{2}(:\d{2})?$' THEN (s.registro->>'hora_inicio')::time
            ELSE '00:00'::time
        END,
        CASE
            WHEN COALESCE(s.registro->>'hora_final', '') ~ '^\d{1,2}:\d{2}(:\d{2})?$' THEN (s.registro->>'hora_final')::time
            ELSE '00:00'::time
        END,
        (s.registro->'operacion')::jsonb,
        s.ts,
        s.ts
    FROM registros_source s
    ORDER BY s.operacion_id, s.source_ordinal
    RETURNING id, operacion_id, external_id
), source_with_ordinal AS (
    SELECT
        m.new_id AS operacion_id,
        t.item,
        t.ordinality AS source_ordinal,
        t.item->'operacion' AS op
    FROM "Operacion_tal_horizontal" o
    JOIN tmp_otv2_map m ON m.legacy_id = o.id
    CROSS JOIN LATERAL jsonb_array_elements(COALESCE(NULLIF(o.registros, ''), '[]')::jsonb) WITH ORDINALITY AS t(item, ordinality)
), inserted_with_ordinal AS (
    SELECT
        r.id,
        r.operacion_id,
        row_number() OVER (PARTITION BY r.operacion_id ORDER BY r.id) AS source_ordinal
    FROM registros_inserted r
)
INSERT INTO operacion_tal_horizontal_registro_detalle (
    registro_id,
    nivel,
    tipo_labor,
    labor,
    ala,
    tal_prod,
    tal_rimados,
    tal_alivio,
    tal_repaso,
    long_barras,
    num_barras,
    tipo_perforacion,
    tipo_perforacion_id,
    observaciones
)
SELECT DISTINCT ON (ri.id)
    ri.id,
    NULLIF(src.op->>'nivel', ''),
    NULLIF(src.op->>'tipo_labor', ''),
    NULLIF(src.op->>'labor', ''),
    NULLIF(src.op->>'ala', ''),
    NULLIF(src.op->>'tal_prod', '')::numeric(10,2),
    NULLIF(src.op->>'tal_rimados', '')::numeric(10,2),
    NULLIF(src.op->>'tal_alivio', '')::numeric(10,2),
    NULLIF(src.op->>'tal_repaso', '')::numeric(10,2),
    NULLIF(src.op->>'long_barras', '')::numeric(10,2),
    NULLIF(src.op->>'num_barras', '')::numeric(10,2),
    NULLIF(src.op->>'tipo_perforacion', ''),
    NULLIF(src.op->>'tipo_perforacion_id', '')::integer,
    NULLIF(src.op->>'observaciones', '')
FROM source_with_ordinal src
JOIN inserted_with_ordinal ri
  ON ri.operacion_id = src.operacion_id
 AND ri.source_ordinal = src.source_ordinal
ORDER BY ri.id
ON CONFLICT (registro_id) DO UPDATE
SET
    nivel = EXCLUDED.nivel,
    tipo_labor = EXCLUDED.tipo_labor,
    labor = EXCLUDED.labor,
    ala = EXCLUDED.ala,
    tal_prod = EXCLUDED.tal_prod,
    tal_rimados = EXCLUDED.tal_rimados,
    tal_alivio = EXCLUDED.tal_alivio,
    tal_repaso = EXCLUDED.tal_repaso,
    long_barras = EXCLUDED.long_barras,
    num_barras = EXCLUDED.num_barras,
    tipo_perforacion = EXCLUDED.tipo_perforacion,
    tipo_perforacion_id = EXCLUDED.tipo_perforacion_id,
    observaciones = EXCLUDED.observaciones;

COMMIT;

BEGIN;

UPDATE operacion_carguio_v2 o
SET seccion_id = s.id
FROM secciones s
WHERE o.seccion IS NOT NULL
  AND btrim(o.seccion) <> ''
  AND (
      (
          position(',' in o.seccion) > 0
          AND s.proceso = TRIM(SPLIT_PART(o.seccion, ',', 1))
          AND s.nombre = TRIM(SPLIT_PART(o.seccion, ',', 2))
      )
      OR (
          position(',' in o.seccion) = 0
          AND s.proceso = 'SCOOPTRAM'
          AND s.nombre = btrim(o.seccion)
      )
  )
  AND o.seccion_id IS DISTINCT FROM s.id;

COMMIT;

BEGIN;

SET LOCAL search_path TO bwqpru6uszd4olgimtbh, public;

-- Ajusta este valor si en checklist_items.proceso usas otro nombre
-- por ejemplo 'JUMBO' en vez de 'PERFORACIÓN HORIZONTAL'.
WITH proceso_catalogo AS (
    SELECT 'PERFORACIÓN HORIZONTAL'::text AS valor
)
UPDATE operacion_tal_horizontal_checklist r
SET checklist_item_id = c.id
FROM operacion_tal_horizontal_v2 o,
     checklist_items c,
     proceso_catalogo pc
WHERE r.operacion_id = o.id
  AND lower(trim(c.proceso)) = lower(trim(pc.valor))
  AND lower(trim(c.categoria)) = lower(trim(r.categoria_snapshot))
  AND lower(trim(c.nombre)) = lower(trim(r.descripcion_snapshot))
  AND r.checklist_item_id IS NULL;

-- Validación rápida: cuántos no matchearon todavía.
-- SELECT count(*)
-- FROM operacion_tal_horizontal_checklist
-- WHERE checklist_item_id IS NULL;

-- Ver cuáles faltan en el catálogo.
-- SELECT categoria_snapshot, descripcion_snapshot
-- FROM operacion_tal_horizontal_checklist
-- WHERE checklist_item_id IS NULL
-- GROUP BY categoria_snapshot, descripcion_snapshot
-- ORDER BY categoria_snapshot, descripcion_snapshot;

COMMIT;

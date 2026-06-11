-- Ajusta este valor si en checklist_items.proceso usas otro nombre.
WITH proceso_catalogo AS (
    SELECT 'SCOOPTRAM'::text AS valor
)
UPDATE operacion_carguio_checklist r
SET checklist_item_id = c.id
FROM operacion_carguio_v2 o,
     checklist_items c,
     proceso_catalogo pc
WHERE r.operacion_id = o.id
  AND lower(trim(c.proceso)) = lower(trim(pc.valor))
  AND lower(trim(c.categoria)) = lower(trim(r.categoria_snapshot))
  AND lower(trim(c.nombre)) = lower(trim(r.descripcion_snapshot))
  AND r.checklist_item_id IS NULL;

-- Validacion rapida:
-- SELECT count(*)
-- FROM operacion_carguio_checklist
-- WHERE checklist_item_id IS NULL;

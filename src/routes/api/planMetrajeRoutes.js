const express = require('express');
const {
    getAllPlanMetraje,
    getPlanMetrajeById,
    createPlanMetraje,
    updatePlanMetraje,
    deletePlanMetraje,
    getPlanMetrajeByYearAndMonth,

} = require('../../controllers/planMetrajeController');
const verificarToken = require('../../middleware/auth');

const router = express.Router();

router.get('/', verificarToken, getAllPlanMetraje);
router.get('/anio/:anio/mes/:mes', verificarToken, getPlanMetrajeByYearAndMonth,
);

router.get('/:id', verificarToken, getPlanMetrajeById);
router.post('/', verificarToken, createPlanMetraje);
router.put('/:id', verificarToken, updatePlanMetraje);
router.delete('/:id', verificarToken, deletePlanMetraje);

module.exports = router;

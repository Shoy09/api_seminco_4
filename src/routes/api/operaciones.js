const express = require('express');
const router = express.Router();
const controller = require('../../controllers/operaciones');

router.get('/horometros/ultimos', controller.obtenerUltimosHorometros);
router.post('/crear', controller.crear);
router.get('/:tipo', controller.obtener);
router.put('/actualizar', controller.actualizar);

module.exports = router;
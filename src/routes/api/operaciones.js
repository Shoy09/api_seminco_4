const express = require('express');
const router = express.Router();
const controller = require('../../controllers/operaciones');
const verificarToken = require('../../middleware/auth');


router.get('/horometros/ultimos', verificarToken, controller.obtenerUltimosHorometros);
router.get('/:tipo/jefe', verificarToken, controller.obtenerPorJefe);
router.post('/crear', verificarToken, controller.crear);
router.get('/:tipo', verificarToken, controller.obtener);
router.put('/actualizar', verificarToken, controller.actualizar);

module.exports = router;
const express = require('express');
const router = express.Router();


const usuariosRoutes = require('../routes/api/UsuarioRoutes');
const authRoutes = require('../routes/api/authRoutes');
const equipoRoutes = require('../routes/api/equipoRoutes');
const estadoRoutes = require('../routes/api/estadoRoutes');
const PlamMensual = require('../routes/api/planMensualRoutes');
const TipoPerfpo = require('../routes/api/tipoPerforacionRoutes');
const PlanMetraje = require('../routes/api/planMetrajeRoutes');
const PlanProduccion = require('../routes/api/planProduccionRoutes');
const fechasplanmensual = require('../routes/api/fechasPlanMensualroutes');
const Checklist = require('../routes/api/checklistItemRoutes');
const tipoEquipoRoutes = require('../routes/api/tipoEquipoRoutes');
const checklistTelemandoRoutes = require('../routes/api/checklistTelemandoRoutes');
const seccionRoutes = require('../routes/api/SeccionRuta');
const operacionesRoutes = require('../controllers/operaciones');

router.use('/usuarios', usuariosRoutes);  
router.use('/auth', authRoutes);  
router.use('/Equipo', equipoRoutes);  
router.use('/estado', estadoRoutes);  
router.use('/TipoPerfpo', TipoPerfpo);  
router.use('/PlamMensual', PlamMensual);  
router.use('/PlanMetraje', PlanMetraje);  
router.use('/PlanProduccion', PlanProduccion); 
router.use('/fechas-plan-mensual', fechasplanmensual)
router.use('/check-list', Checklist); 
router.use('/tipo-equipos', tipoEquipoRoutes); 
router.use('/checklists-telemando', checklistTelemandoRoutes);
router.use('/secciones', seccionRoutes);
router.use('/operaciones', operacionesRoutes);

module.exports = router;

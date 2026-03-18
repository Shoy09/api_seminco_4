const { DataTypes } = require('sequelize');
const sequelize = require('../config/sequelize');

const OperacionCarguio = sequelize.define('OperacionCarguio', {
    id: { type: DataTypes.INTEGER, primaryKey: true, autoIncrement: true },
    fecha: DataTypes.STRING,
    turno: DataTypes.STRING,
    seccion: DataTypes.STRING,
    operador: DataTypes.STRING,
    jefe_guardia: DataTypes.STRING,
    equipo: DataTypes.STRING,
    n_equipo: DataTypes.STRING,
    capacidad: DataTypes.STRING,
    tipo_equipo: DataTypes.STRING,
    registros: DataTypes.TEXT,
    horometros: DataTypes.TEXT,
    condiciones_equipo: DataTypes.TEXT,
    programa_trabajo: DataTypes.TEXT,
    check_list: DataTypes.TEXT,
    check_list_telemando: DataTypes.TEXT,
    control_llantas: DataTypes.TEXT,
    estado: { type: DataTypes.STRING, defaultValue: 'activo' },
    envio: { type: DataTypes.INTEGER, defaultValue: 0 }
}, {
    tableName: 'Operacion_carguio',
    timestamps: false
});

module.exports = OperacionCarguio;
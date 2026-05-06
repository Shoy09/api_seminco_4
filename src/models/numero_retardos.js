const { DataTypes } = require('sequelize');
const sequelize = require('../config/sequelize');

const NumeroRetardos = sequelize.define('NumeroRetardos', {
    id: {
        type: DataTypes.INTEGER,
        primaryKey: true,
        autoIncrement: true
    },
    longitud: {
        type: DataTypes.DECIMAL(10, 2), // soporta enteros y decimales
        allowNull: false
    },
    tipo: {
        type: DataTypes.STRING,
        allowNull: false
    },
    codigo: {
        type: DataTypes.STRING,
        allowNull: false,
        unique: true // opcional pero recomendado si es identificador
    }
}, {
    tableName: 'numero_retardos',
    timestamps: false
});

module.exports = NumeroRetardos;
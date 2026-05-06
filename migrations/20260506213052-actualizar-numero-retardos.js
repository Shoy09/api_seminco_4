'use strict';

module.exports = {
  async up(queryInterface, Sequelize) {

    // 1. Agregar nuevas columnas
    await queryInterface.addColumn('numero_retardos', 'longitud', {
      type: Sequelize.DECIMAL(10, 2),
      allowNull: false,
      defaultValue: 0 // temporal para no romper registros existentes
    });

    await queryInterface.addColumn('numero_retardos', 'tipo', {
      type: Sequelize.STRING,
      allowNull: false,
      defaultValue: 'default'
    });

    await queryInterface.addColumn('numero_retardos', 'codigo', {
      type: Sequelize.STRING,
      allowNull: false,
      defaultValue: 'TEMP'
    });

    // 2. Eliminar columnas antiguas
    await queryInterface.removeColumn('numero_retardos', 'mes');
    await queryInterface.removeColumn('numero_retardos', 'anio');
    await queryInterface.removeColumn('numero_retardos', 'cantidad');

    // 3. (Opcional pero recomendado) quitar defaults y poner unique
    await queryInterface.changeColumn('numero_retardos', 'codigo', {
      type: Sequelize.STRING,
      allowNull: false,
      unique: true
    });

    await queryInterface.changeColumn('numero_retardos', 'tipo', {
      type: Sequelize.STRING,
      allowNull: false
    });

    await queryInterface.changeColumn('numero_retardos', 'longitud', {
      type: Sequelize.DECIMAL(10, 2),
      allowNull: false
    });
  },

  async down(queryInterface, Sequelize) {

    // Revertir: volver a estructura antigua

    await queryInterface.addColumn('numero_retardos', 'mes', {
      type: Sequelize.STRING,
      allowNull: false,
      defaultValue: 'ENERO'
    });

    await queryInterface.addColumn('numero_retardos', 'anio', {
      type: Sequelize.INTEGER,
      allowNull: false,
      defaultValue: 2024
    });

    await queryInterface.addColumn('numero_retardos', 'cantidad', {
      type: Sequelize.INTEGER,
      allowNull: false,
      defaultValue: 0
    });

    await queryInterface.removeColumn('numero_retardos', 'longitud');
    await queryInterface.removeColumn('numero_retardos', 'tipo');
    await queryInterface.removeColumn('numero_retardos', 'codigo');
  }
};
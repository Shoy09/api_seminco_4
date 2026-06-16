'use strict';

module.exports = {
  up: async (queryInterface, Sequelize) => {
    await queryInterface.createTable('pdfs', {
      id: {
        type: Sequelize.INTEGER,
        primaryKey: true,
        autoIncrement: true
      },
      proceso: {
        type: Sequelize.STRING(100),
        allowNull: false
      },
      mes: {
        type: Sequelize.ENUM(
          'ENERO',
          'FEBRERO',
          'MARZO',
          'ABRIL',
          'MAYO',
          'JUNIO',
          'JULIO',
          'AGOSTO',
          'SEPTIEMBRE',
          'OCTUBRE',
          'NOVIEMBRE',
          'DICIEMBRE'
        ),
        allowNull: false
      },
      url_pdf: {
        type: Sequelize.STRING(255),
        allowNull: false
      },
      labor: {
        type: Sequelize.TEXT,
        allowNull: true
      },
      createdAt: {
        type: Sequelize.DATE,
        allowNull: false,
        defaultValue: Sequelize.literal('CURRENT_TIMESTAMP')
      },
      updatedAt: {
        type: Sequelize.DATE,
        allowNull: false,
        defaultValue: Sequelize.literal('CURRENT_TIMESTAMP')
      }
    });
  },

  down: async (queryInterface, Sequelize) => {
    await queryInterface.dropTable('pdfs');

    // Para PostgreSQL es necesario eliminar el ENUM manualmente
    if (queryInterface.sequelize.getDialect() === 'postgres') {
      await queryInterface.sequelize.query(
        'DROP TYPE IF EXISTS "enum_pdfs_mes";'
      );
    }
  }
};
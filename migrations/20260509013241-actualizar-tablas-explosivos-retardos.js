'use strict';

module.exports = {
  async up(queryInterface, Sequelize) {
    // ==============================================
    // 1. MODIFICAR TABLA: nube_detalle_despacho_explosivos
    // ==============================================
    
    // Verificar si la tabla existe (opcional, para evitar errores)
    const tables = await queryInterface.showAllTables();
    
    if (tables.includes('nube_detalle_despacho_explosivos')) {
      // Eliminar columnas antiguas
      await queryInterface.removeColumn('nube_detalle_despacho_explosivos', 'numero');
      await queryInterface.removeColumn('nube_detalle_despacho_explosivos', 'ms_cant1');
      await queryInterface.removeColumn('nube_detalle_despacho_explosivos', 'lp_cant1');
      
      // Agregar nuevas columnas
      await queryInterface.addColumn('nube_detalle_despacho_explosivos', 'longitud', {
        type: Sequelize.FLOAT,
        allowNull: false,
        defaultValue: 0.0
      });
      
      await queryInterface.addColumn('nube_detalle_despacho_explosivos', 'tipo', {
        type: Sequelize.STRING(50),
        allowNull: false,
        defaultValue: 'Milisegundo'
      });
      
      await queryInterface.addColumn('nube_detalle_despacho_explosivos', 'retardos', {
        type: Sequelize.JSON,
        allowNull: false,
        defaultValue: []
      });
    }

    // ==============================================
    // 2. MODIFICAR TABLA: nube_detalle_devoluciones_explosivos
    // ==============================================
    
    if (tables.includes('nube_detalle_devoluciones_explosivos')) {
      // Eliminar columnas antiguas
      await queryInterface.removeColumn('nube_detalle_devoluciones_explosivos', 'numero');
      await queryInterface.removeColumn('nube_detalle_devoluciones_explosivos', 'ms_cant1');
      await queryInterface.removeColumn('nube_detalle_devoluciones_explosivos', 'lp_cant1');
      
      // Agregar nuevas columnas
      await queryInterface.addColumn('nube_detalle_devoluciones_explosivos', 'longitud', {
        type: Sequelize.FLOAT,
        allowNull: false,
        defaultValue: 0.0
      });
      
      await queryInterface.addColumn('nube_detalle_devoluciones_explosivos', 'tipo', {
        type: Sequelize.STRING(50),
        allowNull: false,
        defaultValue: 'Milisegundo'
      });
      
      await queryInterface.addColumn('nube_detalle_devoluciones_explosivos', 'retardos', {
        type: Sequelize.JSON,
        allowNull: false,
        defaultValue: []
      });
    }

    // ==============================================
    // 3. OPCIONAL: Agregar índice para mejor rendimiento
    // ==============================================
    
    if (tables.includes('nube_detalle_despacho_explosivos')) {
      await queryInterface.addIndex('nube_detalle_despacho_explosivos', ['id_despacho']);
      await queryInterface.addIndex('nube_detalle_despacho_explosivos', ['tipo']);
    }
    
    if (tables.includes('nube_detalle_devoluciones_explosivos')) {
      await queryInterface.addIndex('nube_detalle_devoluciones_explosivos', ['id_devolucion']);
      await queryInterface.addIndex('nube_detalle_devoluciones_explosivos', ['tipo']);
    }
  },

  async down(queryInterface, Sequelize) {
    // ==============================================
    // REVERTIR CAMBIOS (en caso de rollback)
    // ==============================================
    
    const tables = await queryInterface.showAllTables();
    
    // Revertir cambios en nube_detalle_despacho_explosivos
    if (tables.includes('nube_detalle_despacho_explosivos')) {
      // Eliminar columnas nuevas
      await queryInterface.removeColumn('nube_detalle_despacho_explosivos', 'longitud');
      await queryInterface.removeColumn('nube_detalle_despacho_explosivos', 'tipo');
      await queryInterface.removeColumn('nube_detalle_despacho_explosivos', 'retardos');
      
      // Restaurar columnas antiguas
      await queryInterface.addColumn('nube_detalle_despacho_explosivos', 'numero', {
        type: Sequelize.INTEGER,
        allowNull: false,
        defaultValue: 0
      });
      
      await queryInterface.addColumn('nube_detalle_despacho_explosivos', 'ms_cant1', {
        type: Sequelize.STRING,
        allowNull: false,
        defaultValue: ''
      });
      
      await queryInterface.addColumn('nube_detalle_despacho_explosivos', 'lp_cant1', {
        type: Sequelize.STRING,
        allowNull: false,
        defaultValue: ''
      });
    }
    
    // Revertir cambios en nube_detalle_devoluciones_explosivos
    if (tables.includes('nube_detalle_devoluciones_explosivos')) {
      // Eliminar columnas nuevas
      await queryInterface.removeColumn('nube_detalle_devoluciones_explosivos', 'longitud');
      await queryInterface.removeColumn('nube_detalle_devoluciones_explosivos', 'tipo');
      await queryInterface.removeColumn('nube_detalle_devoluciones_explosivos', 'retardos');
      
      // Restaurar columnas antiguas
      await queryInterface.addColumn('nube_detalle_devoluciones_explosivos', 'numero', {
        type: Sequelize.INTEGER,
        allowNull: false,
        defaultValue: 0
      });
      
      await queryInterface.addColumn('nube_detalle_devoluciones_explosivos', 'ms_cant1', {
        type: Sequelize.STRING,
        allowNull: false,
        defaultValue: ''
      });
      
      await queryInterface.addColumn('nube_detalle_devoluciones_explosivos', 'lp_cant1', {
        type: Sequelize.STRING,
        allowNull: false,
        defaultValue: ''
      });
    }
  }
};
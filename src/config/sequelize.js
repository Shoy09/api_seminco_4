const { Sequelize } = require('sequelize');
require('dotenv').config();

const sequelize = new Sequelize(
    process.env.DB_NAME,
    process.env.DB_USER,
    process.env.DB_PASSWORD,
    {
        host: process.env.DB_HOST,
        dialect: 'mysql',
        port: process.env.DB_PORT || 3306,
        pool: {
            max: 5,       // máximo de conexiones simultáneas (ajustar al límite de tu DB)
            min: 0,       // mínimo de conexiones en el pool
            acquire: 30000, // tiempo máximo en ms que sequelize esperará por una conexión disponible
            idle: 10000     // tiempo máximo en ms que una conexión puede estar inactiva antes de ser liberada
        },
        define: {
            // Opcional: evita crear timestamps automáticos si no los necesitas
            timestamps: false
        }
    }
);

// Prueba la conexión
sequelize.authenticate()
    .then(() => {
        console.log('Conexión a la base de datos establecida con éxito');
    })
    .catch((err) => {
        console.error('No se pudo conectar a la base de datos:', err);
    });

module.exports = sequelize;
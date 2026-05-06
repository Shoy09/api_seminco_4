const NumeroRetardos = require('../../src/models/numero_retardos');

const numeroRetardosController = {

    getAll: async (req, res) => {
        try {
            const registros = await NumeroRetardos.findAll();
            res.json(registros);
        } catch (error) {
            res.status(500).json({ error: 'Error al obtener los registros' });
        }
    },

    create: async (req, res) => {
        try {
            const { longitud, tipo, codigo } = req.body;

            // Validación básica
            if (longitud == null || !tipo || !codigo) {
                return res.status(400).json({
                    error: 'longitud, tipo y codigo son obligatorios'
                });
            }

            const registro = await NumeroRetardos.create({
                longitud,
                tipo,
                codigo
            });

            res.status(201).json({
                message: 'Registro creado exitosamente',
                registro
            });

        } catch (error) {
            console.error(error);
            res.status(500).json({
                error: 'Error al crear el registro',
                details: error.message
            });
        }
    },

    update: async (req, res) => {
        try {
            const { id } = req.params;
            const { longitud, tipo, codigo } = req.body;

            const registro = await NumeroRetardos.findByPk(id);

            if (!registro) {
                return res.status(404).json({ error: 'Registro no encontrado' });
            }

            await registro.update({
                longitud,
                tipo,
                codigo
            });

            res.json(registro);

        } catch (error) {
            res.status(500).json({ error: 'Error al actualizar el registro' });
        }
    },

    delete: async (req, res) => {
        try {
            const { id } = req.params;

            const registro = await NumeroRetardos.findByPk(id);

            if (!registro) {
                return res.status(404).json({ error: 'Registro no encontrado' });
            }

            await registro.destroy();

            res.json({ message: 'Registro eliminado correctamente' });

        } catch (error) {
            res.status(500).json({ error: 'Error al eliminar el registro' });
        }
    },

};

module.exports = numeroRetardosController;
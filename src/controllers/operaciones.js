const modelos = require('../models/indexOperaciones');

function obtenerModelo(tipo) {
  const modelo = modelos[tipo];
  if (!modelo) throw new Error('Tipo de operación inválido');
  return modelo;
}

module.exports = {

  // ✅ CREAR (uno o varios)
  async crear(req, res) {
    try {
      const { tipo, data } = req.body;
      const Modelo = obtenerModelo(tipo);

      let resultado;

      if (Array.isArray(data)) {
        // múltiples registros
        resultado = await Modelo.bulkCreate(data);
      } else {
        // un solo registro
        resultado = await Modelo.create(data);
      }

      res.json({ ok: true, data: resultado });

    } catch (error) {
      res.status(500).json({ ok: false, error: error.message });
    }
  },

  // ✅ GET (con filtros)
  async obtener(req, res) {
    try {
      const { tipo } = req.params;
      const { estado, envio, limit = 50, offset = 0 } = req.query;

      const Modelo = obtenerModelo(tipo);

      let where = {};
      if (estado) where.estado = estado;
      if (envio) where.envio = envio;

      const data = await Modelo.findAll({
        where,
        limit: parseInt(limit),
        offset: parseInt(offset),
        order: [['id', 'DESC']]
      });

      res.json({ ok: true, data });

    } catch (error) {
      res.status(500).json({ ok: false, error: error.message });
    }
  },

  // ✅ UPDATE (uno o varios)
  async actualizar(req, res) {
    try {
      const { tipo, data } = req.body;
      const Modelo = obtenerModelo(tipo);

      let resultado;

      if (Array.isArray(data)) {
        // múltiples updates
        const updates = data.map(item =>
          Modelo.update(item, { where: { id: item.id } })
        );
        resultado = await Promise.all(updates);
      } else {
        // uno solo
        const { id, ...rest } = data;
        resultado = await Modelo.update(rest, {
          where: { id }
        });
      }

      res.json({ ok: true, data: resultado });

    } catch (error) {
      res.status(500).json({ ok: false, error: error.message });
    }
  }

};
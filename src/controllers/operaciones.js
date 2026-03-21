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

    /// 🔥 FUNCIÓN SEGURA
    const parseJSON = (value) => {
      try {
        return JSON.parse(value);
      } catch {
        return value;
      }
    };

    /// 🔥 CAMPOS REALES DEL MODELO
    const camposModelo = Object.keys(Modelo.rawAttributes);

    const dataFormateada = data.map(item => {
      const obj = item.toJSON();
      const resultado = {};

      /// 🔥 SOLO RECORRE CAMPOS REALES
      camposModelo.forEach(campo => {
        let valor = obj[campo];

        /// 🔥 SOLO PARSEA SI ES STRING JSON
        if (typeof valor === 'string') {
          try {
            const parsed = JSON.parse(valor);

            /// solo reemplaza si realmente es JSON (array u objeto)
            if (typeof parsed === 'object') {
              valor = parsed;
            }
          } catch (_) {}
        }

        resultado[campo] = valor;
      });

      return resultado;
    });

    res.json({ ok: true, data: dataFormateada });

  } catch (error) {
    res.status(500).json({ ok: false, error: error.message });
  }
},

// ✅ GET HORÓMETROS (último por cada operación - SECUENCIAL)
async obtenerUltimosHorometros(req, res) {
  try {

    const resultado = {};

    for (const [tipo, Modelo] of Object.entries(modelos)) {

      // 🔥 traer SOLO el campo necesario (más ligero)
      const ultimo = await Modelo.findOne({
        attributes: ['horometros'],
        order: [['id', 'DESC']]
      });

      if (!ultimo) {
        resultado[tipo] = null;
        continue;
      }

      let horometros = ultimo.get('horometros');

      // 🔥 parse seguro
      if (typeof horometros === 'string') {
        try {
          const parsed = JSON.parse(horometros);
          if (parsed && typeof parsed === 'object') {
            horometros = parsed;
          }
        } catch (_) {
          // si falla, se queda como string
        }
      }

      resultado[tipo] = horometros;
    }

    res.json({
      ok: true,
      data: resultado
    });

  } catch (error) {
    console.error('❌ Error en obtenerUltimosHorometros:', error);
    res.status(500).json({
      ok: false,
      error: error.message
    });
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
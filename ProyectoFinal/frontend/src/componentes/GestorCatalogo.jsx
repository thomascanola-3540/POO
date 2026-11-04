import { useState } from 'react';
import { useDatos } from '../hooks/useDatos.js';
import Alerta from './Alerta.jsx';
import Campo from './Campo.jsx';
import EstadoCarga from './EstadoCarga.jsx';

// Tabla con formulario de crear y editar. Departamentos, ciudades, aves y roles funcionan igual,
// así que cada uno solo describe sus campos y columnas, y le pasa sus funciones de la API.
export default function GestorCatalogo({
  titulo, descripcion, campos, columnas, cargar, crear, actualizar, cambiarEstado, estaActivo,
}) {
  const { datos: filas, cargando, error, recargar } = useDatos(cargar);
  const [editando, setEditando] = useState(null);
  const [valores, setValores] = useState(valoresVacios(campos));
  const [enviando, setEnviando] = useState(false);
  const [errorFormulario, setErrorFormulario] = useState('');
  const [aviso, setAviso] = useState('');

  function empezarEdicion(fila) {
    setEditando(fila);
    setValores(Object.fromEntries(campos.map((campo) => [campo.nombre, campo.valorDe(fila) ?? ''])));
    setErrorFormulario('');
    setAviso('');
  }

  function limpiar() {
    setEditando(null);
    setValores(valoresVacios(campos));
    setErrorFormulario('');
  }

  async function guardar(evento) {
    evento.preventDefault();
    setEnviando(true);
    setErrorFormulario('');
    setAviso('');
    try {
      if (editando) {
        await actualizar(editando.id, valores);
        setAviso('Cambios guardados.');
      } else {
        await crear(valores);
        setAviso('Registro creado.');
      }
      limpiar();
      recargar();
    } catch (problema) {
      setErrorFormulario(problema.message);
    } finally {
      setEnviando(false);
    }
  }

  async function alternarEstado(fila) {
    setErrorFormulario('');
    setAviso('');
    try {
      await cambiarEstado(fila.id, !estaActivo(fila));
      recargar();
    } catch (problema) {
      setErrorFormulario(problema.message);
    }
  }

  return (
    <section className="catalogo">
      <h2>{titulo}</h2>
      {descripcion && <p className="texto-suave">{descripcion}</p>}

      <form className="tarjeta catalogo-formulario" onSubmit={guardar}>
        <h3>{editando ? 'Editar registro' : 'Agregar nuevo'}</h3>
        <div className="rejilla-campos">
          {campos.map((campo) => (
            <Campo key={campo.nombre} etiqueta={campo.etiqueta} opcional={campo.opcional}>
              {campo.opciones ? (
                <select
                  required={!campo.opcional}
                  value={valores[campo.nombre]}
                  onChange={(e) => setValores({ ...valores, [campo.nombre]: e.target.value })}
                >
                  <option value="">Selecciona</option>
                  {campo.opciones.map((opcion) => <option key={opcion.valor} value={opcion.valor}>{opcion.texto}</option>)}
                </select>
              ) : (
                <input
                  required={!campo.opcional}
                  maxLength={campo.longitud ?? 150}
                  disabled={editando && campo.bloqueado?.(editando)}
                  value={valores[campo.nombre]}
                  onChange={(e) => setValores({ ...valores, [campo.nombre]: e.target.value })}
                />
              )}
            </Campo>
          ))}
        </div>
        <Alerta>{errorFormulario}</Alerta>
        <Alerta tipo="exito">{aviso}</Alerta>
        <div className="fila-botones">
          <button type="submit" className="boton" disabled={enviando}>{editando ? 'Guardar cambios' : 'Agregar'}</button>
          {editando && <button type="button" className="boton boton-texto" onClick={limpiar}>Cancelar</button>}
        </div>
      </form>

      <EstadoCarga cargando={cargando && !filas} error={error} reintentar={recargar}>
        <div className="tabla-marco">
          <table className="tabla">
            <thead>
              <tr>
                {columnas.map((columna) => <th key={columna.titulo} scope="col">{columna.titulo}</th>)}
                {estaActivo && <th scope="col">Estado</th>}
                <th scope="col"><span className="solo-lector">Acciones</span></th>
              </tr>
            </thead>
            <tbody>
              {filas?.map((fila) => (
                <tr key={fila.id}>
                  {columnas.map((columna) => <td key={columna.titulo}>{columna.valor(fila)}</td>)}
                  {estaActivo && (
                    <td>
                      <span className={`insignia ${estaActivo(fila) ? 'insignia-validado' : 'insignia-inactivo'}`}>
                        {estaActivo(fila) ? 'Activo' : 'Inactivo'}
                      </span>
                    </td>
                  )}
                  <td className="tabla-acciones">
                    <button type="button" className="boton boton-texto boton-pequeno" onClick={() => empezarEdicion(fila)}>Editar</button>
                    {cambiarEstado && (
                      <button type="button" className="boton boton-texto boton-pequeno" onClick={() => alternarEstado(fila)}>
                        {estaActivo(fila) ? 'Desactivar' : 'Activar'}
                      </button>
                    )}
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </EstadoCarga>
    </section>
  );
}

function valoresVacios(campos) {
  return Object.fromEntries(campos.map((campo) => [campo.nombre, '']));
}

import { useCallback, useState } from 'react';
import { Link, Navigate } from 'react-router-dom';
import { obtenerAves } from '../api/aves.js';
import { obtenerCiudadesPorDepartamento } from '../api/ciudades.js';
import { obtenerDepartamentos } from '../api/departamentos.js';
import { registrarEvento } from '../api/eventos.js';
import Alerta from '../componentes/Alerta.jsx';
import Campo from '../componentes/Campo.jsx';
import EstadoCarga from '../componentes/EstadoCarga.jsx';
import { useSesion } from '../contexto/ContextoSesion.jsx';
import { useDatos } from '../hooks/useDatos.js';
import { fechaParaCampo, formatearTamano } from '../utilidades/formato.js';
import { MAXIMO_IMAGENES, problemaDeArchivo, REGLAS_AUDIO, REGLAS_IMAGEN } from '../utilidades/reglasArchivos.js';

const FORMULARIO_VACIO = {
  departamentoId: '', ciudadId: '', latitud: '', longitud: '', direccion: '', descripcionLugar: '', notas: '', nombreHipotetico: '',
};

// CU04: registrar un evento de escucha o avistamiento (RF04, RF05 y RF06).
export default function RegistrarEvento() {
  const { usuario, esAdministrador } = useSesion();
  const [datos, setDatos] = useState(FORMULARIO_VACIO);
  const [fechaHora, setFechaHora] = useState(() => fechaParaCampo());
  const [audio, setAudio] = useState(null);
  const [imagenes, setImagenes] = useState([]);
  const [enviando, setEnviando] = useState(false);
  const [error, setError] = useState('');
  const [errorArchivo, setErrorArchivo] = useState('');
  const [avisoUbicacion, setAvisoUbicacion] = useState('');
  const [registrado, setRegistrado] = useState(null);

  const cargarDepartamentos = useCallback(() => obtenerDepartamentos(), []);
  const cargarAves = useCallback(() => obtenerAves(), []);
  const cargarCiudades = useCallback(
    () => (datos.departamentoId ? obtenerCiudadesPorDepartamento(datos.departamentoId) : Promise.resolve([])),
    [datos.departamentoId],
  );

  const departamentos = useDatos(cargarDepartamentos);
  const aves = useDatos(cargarAves);
  const ciudades = useDatos(cargarCiudades);

  // Según la matriz del documento (2.4), el administrador no registra eventos.
  if (esAdministrador) {
    return <Navigate to="/admin/eventos" replace />;
  }

  const cambiar = (campo) => (e) => setDatos({ ...datos, [campo]: e.target.value });

  function elegirAudio(e) {
    const archivo = e.target.files[0] ?? null;
    const problema = archivo && problemaDeArchivo(archivo, REGLAS_AUDIO);
    setErrorArchivo(problema ?? '');
    setAudio(problema ? null : archivo);
    if (problema) e.target.value = '';
  }

  function agregarImagenes(e) {
    const nuevas = [...e.target.files];
    e.target.value = '';
    const problema = nuevas.map((archivo) => problemaDeArchivo(archivo, REGLAS_IMAGEN)).find(Boolean);
    if (problema) {
      setErrorArchivo(problema);
    } else if (imagenes.length + nuevas.length > MAXIMO_IMAGENES) {
      setErrorArchivo(`Solo se permiten ${MAXIMO_IMAGENES} imágenes por evento.`);
    } else {
      setErrorArchivo('');
      setImagenes([...imagenes, ...nuevas]);
    }
  }

  function usarMiUbicacion() {
    if (!navigator.geolocation) {
      setAvisoUbicacion('Tu navegador no permite obtener la ubicación. Escribe las coordenadas.');
      return;
    }
    setAvisoUbicacion('Buscando tu ubicación…');
    navigator.geolocation.getCurrentPosition(
      ({ coords }) => {
        setDatos((actual) => ({ ...actual, latitud: coords.latitude.toFixed(6), longitud: coords.longitude.toFixed(6) }));
        setAvisoUbicacion('Coordenadas agregadas.');
      },
      () => setAvisoUbicacion('No se pudo obtener tu ubicación. Escribe las coordenadas.'),
    );
  }

  function reiniciar() {
    setDatos(FORMULARIO_VACIO);
    setFechaHora(fechaParaCampo());
    setAudio(null);
    setImagenes([]);
    setAvisoUbicacion('');
    setRegistrado(null);
  }

  async function enviar(evento) {
    evento.preventDefault();
    if (!audio) {
      setError('Debes adjuntar el archivo de audio.');
      return;
    }

    // Los nombres deben coincidir con EventoDTO y con los parámetros del controlador.
    const formulario = new FormData();
    formulario.append('FechaHora', fechaHora);
    formulario.append('Longitud', datos.longitud);
    formulario.append('Latitud', datos.latitud);
    formulario.append('CiudadId', datos.ciudadId);
    if (datos.direccion.trim()) formulario.append('Direccion', datos.direccion.trim());
    if (datos.descripcionLugar.trim()) formulario.append('DescripcionLugar', datos.descripcionLugar.trim());
    if (datos.notas.trim()) formulario.append('Notas', datos.notas.trim());
    if (datos.nombreHipotetico.trim()) formulario.append('NombreHipotetico', datos.nombreHipotetico.trim());
    formulario.append('audio', audio);
    imagenes.forEach((imagen) => formulario.append('imagenes', imagen));

    setEnviando(true);
    setError('');
    try {
      setRegistrado(await registrarEvento(formulario));
      window.scrollTo({ top: 0 });
    } catch (problema) {
      setError(problema.message);
    } finally {
      setEnviando(false);
    }
  }

  if (registrado) {
    return (
      <section className="formulario-corto">
        <div className="tarjeta confirmacion">
          <h1>¡Gracias por tu aporte!</h1>
          <p>Tu evento #{registrado.id} en {registrado.ciudad}, {registrado.departamento} quedó registrado y pendiente de validación.</p>
          {usuario ? (
            <p>También lo encuentras en tu perfil.</p>
          ) : (
            <p>Si creas una cuenta, tus próximos aportes quedarán en tu perfil y podrás escuchar los de la comunidad.</p>
          )}
          <div className="fila-botones">
            <button type="button" className="boton" onClick={reiniciar}>Registrar otro</button>
            <Link className="boton boton-secundario" to={usuario ? '/perfil' : '/crear-cuenta'}>
              {usuario ? 'Ver mi perfil' : 'Crear cuenta'}
            </Link>
          </div>
        </div>
      </section>
    );
  }

  return (
    <section>
      <h1>Registrar un evento</h1>
      <p className="texto-suave">
        {usuario ? `Tu aporte quedará asociado a tu perfil, ${usuario.nombres}.` : 'Estás aportando como colaborador anónimo.'}
      </p>

      <EstadoCarga cargando={departamentos.cargando} error={departamentos.error} reintentar={departamentos.recargar}>
        <form className="formulario-evento" onSubmit={enviar}>
          <fieldset className="tarjeta">
            <legend>El audio</legend>
            <Campo etiqueta="Archivo de audio" ayuda={`${REGLAS_AUDIO.extensiones.join(', ')} · máximo 15 MB`}>
              <input type="file" required accept={REGLAS_AUDIO.extensiones.join(',')} onChange={elegirAudio} />
            </Campo>
            {audio && <p className="texto-suave">{audio.name} · {formatearTamano(audio.size)}</p>}

            <Campo etiqueta="Imágenes" opcional ayuda={`${REGLAS_IMAGEN.extensiones.join(', ')} · máximo 2 MB cada una y ${MAXIMO_IMAGENES} en total`}>
              <input type="file" multiple accept={REGLAS_IMAGEN.extensiones.join(',')} onChange={agregarImagenes} />
            </Campo>
            {imagenes.length > 0 && (
              <ul className="lista-archivos">
                {imagenes.map((imagen, indice) => (
                  <li key={`${imagen.name}-${indice}`}>
                    <span>{imagen.name} · {formatearTamano(imagen.size)}</span>
                    <button
                      type="button"
                      className="boton boton-texto boton-pequeno"
                      onClick={() => setImagenes(imagenes.filter((_, posicion) => posicion !== indice))}
                    >
                      Quitar
                    </button>
                  </li>
                ))}
              </ul>
            )}
            <Alerta>{errorArchivo}</Alerta>
          </fieldset>

          <fieldset className="tarjeta">
            <legend>Cuándo y dónde</legend>
            <div className="rejilla-campos">
              <Campo etiqueta="Fecha y hora">
                <input type="datetime-local" required max={fechaParaCampo()} value={fechaHora} onChange={(e) => setFechaHora(e.target.value)} />
              </Campo>
              <Campo etiqueta="Departamento">
                <select
                  required
                  value={datos.departamentoId}
                  onChange={(e) => setDatos({ ...datos, departamentoId: e.target.value, ciudadId: '' })}
                >
                  <option value="">Selecciona</option>
                  {departamentos.datos?.map((d) => <option key={d.id} value={d.id}>{d.nombre}</option>)}
                </select>
              </Campo>
              <Campo etiqueta="Ciudad">
                <select required disabled={!datos.departamentoId || ciudades.cargando} value={datos.ciudadId} onChange={cambiar('ciudadId')}>
                  <option value="">{datos.departamentoId ? 'Selecciona' : 'Primero elige el departamento'}</option>
                  {ciudades.datos?.map((c) => <option key={c.id} value={c.id}>{c.nombre}</option>)}
                </select>
              </Campo>
              <Campo etiqueta="Latitud" ayuda="Entre -90 y 90">
                <input type="number" required step="any" min="-90" max="90" value={datos.latitud} onChange={cambiar('latitud')} />
              </Campo>
              <Campo etiqueta="Longitud" ayuda="Entre -180 y 180">
                <input type="number" required step="any" min="-180" max="180" value={datos.longitud} onChange={cambiar('longitud')} />
              </Campo>
              <div className="campo campo-boton">
                <button type="button" className="boton boton-secundario" onClick={usarMiUbicacion}>Usar mi ubicación</button>
                {avisoUbicacion && <span className="campo-ayuda" role="status">{avisoUbicacion}</span>}
              </div>
            </div>
            <Alerta>{ciudades.error}</Alerta>
            <Campo etiqueta="Dirección" opcional>
              <input maxLength={250} value={datos.direccion} onChange={cambiar('direccion')} />
            </Campo>
            <Campo etiqueta="Descripción del lugar" opcional>
              <input maxLength={500} placeholder="Ej.: borde de quebrada, cerca al sendero" value={datos.descripcionLugar} onChange={cambiar('descripcionLugar')} />
            </Campo>
          </fieldset>

          <fieldset className="tarjeta">
            <legend>Lo que observaste</legend>
            <Campo etiqueta="¿Qué ave crees que es?" opcional ayuda="Es solo una sugerencia; la identificación definitiva se hace al validar.">
              <input list="catalogo-aves" maxLength={150} value={datos.nombreHipotetico} onChange={cambiar('nombreHipotetico')} />
              <datalist id="catalogo-aves">
                {aves.datos?.map((ave) => <option key={ave.id} value={ave.nombreComun}>{ave.nombreCientifico}</option>)}
              </datalist>
            </Campo>
            <Campo etiqueta="Notas" opcional>
              <textarea rows={3} maxLength={2000} value={datos.notas} onChange={cambiar('notas')} />
            </Campo>
          </fieldset>

          <Alerta>{error}</Alerta>
          <button type="submit" className="boton boton-grande" disabled={enviando}>
            {enviando ? 'Enviando…' : 'Registrar evento'}
          </button>
        </form>
      </EstadoCarga>
    </section>
  );
}

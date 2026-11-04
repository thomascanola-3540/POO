import { useCallback, useState } from 'react';
import { Link } from 'react-router-dom';
import { actualizarPerfil, desvincularEvento, obtenerMisEventos } from '../api/perfil.js';
import Alerta from '../componentes/Alerta.jsx';
import Campo from '../componentes/Campo.jsx';
import Dialogo from '../componentes/Dialogo.jsx';
import EstadoCarga from '../componentes/EstadoCarga.jsx';
import ListaEventos from '../componentes/ListaEventos.jsx';
import { useSesion } from '../contexto/ContextoSesion.jsx';
import { useDatos } from '../hooks/useDatos.js';

// RF03, RF07 y RN05.
export default function MiPerfil() {
  const { usuario, actualizarUsuario } = useSesion();
  const [nombres, setNombres] = useState(usuario.nombres);
  const [apellidos, setApellidos] = useState(usuario.apellidos);
  const [guardando, setGuardando] = useState(false);
  const [error, setError] = useState('');
  const [aviso, setAviso] = useState('');
  const [porDesvincular, setPorDesvincular] = useState(null);
  const [errorDesvincular, setErrorDesvincular] = useState('');

  const cargar = useCallback(() => obtenerMisEventos(), []);
  const { datos: eventos, cargando, error: errorEventos, recargar } = useDatos(cargar);

  async function guardar(evento) {
    evento.preventDefault();
    setGuardando(true);
    setError('');
    setAviso('');
    try {
      actualizarUsuario(await actualizarPerfil({ nombres, apellidos }));
      setAviso('Perfil actualizado.');
    } catch (problema) {
      setError(problema.message);
    } finally {
      setGuardando(false);
    }
  }

  async function confirmarDesvincular() {
    setErrorDesvincular('');
    try {
      await desvincularEvento(porDesvincular.id);
      setPorDesvincular(null);
      recargar();
    } catch (problema) {
      setErrorDesvincular(problema.message);
    }
  }

  return (
    <section>
      <h1>Mi perfil</h1>
      <div className="perfil">
        <form className="tarjeta perfil-datos" onSubmit={guardar}>
          <h2>Mis datos</h2>
          <Campo etiqueta="Nombres">
            <input required maxLength={100} value={nombres} onChange={(e) => setNombres(e.target.value)} />
          </Campo>
          <Campo etiqueta="Apellidos">
            <input required maxLength={100} value={apellidos} onChange={(e) => setApellidos(e.target.value)} />
          </Campo>
          <Campo etiqueta="Correo">
            <input value={usuario.correo} disabled />
          </Campo>
          <Alerta>{error}</Alerta>
          <Alerta tipo="exito">{aviso}</Alerta>
          <button type="submit" className="boton" disabled={guardando}>Guardar</button>
        </form>

        <div className="perfil-eventos">
          <h2>Mis eventos</h2>
          <EstadoCarga cargando={cargando && !eventos} error={errorEventos} reintentar={recargar}>
            {eventos?.length === 0 ? (
              <div className="panel-vacio">
                <p>Aún no tienes eventos en tu perfil.</p>
                <Link to="/registrar-evento" className="boton">Registrar mi primer evento</Link>
              </div>
            ) : (
              <ListaEventos
                eventos={eventos ?? []}
                acciones={(evento) => (
                  <button type="button" className="boton boton-texto boton-pequeno" onClick={() => setPorDesvincular(evento)}>
                    Desvincular de mi perfil
                  </button>
                )}
              />
            )}
          </EstadoCarga>
        </div>
      </div>

      {porDesvincular && (
        <Dialogo titulo="¿Desvincular este evento?" alCerrar={() => setPorDesvincular(null)}>
          <p>Dejará de aparecer en tu perfil, pero seguirá haciendo parte del dataset. Esta acción no se puede deshacer.</p>
          <Alerta>{errorDesvincular}</Alerta>
          <div className="dialogo-acciones">
            <button type="button" className="boton boton-texto" onClick={() => setPorDesvincular(null)}>Cancelar</button>
            <button type="button" className="boton boton-peligro" onClick={confirmarDesvincular}>Desvincular</button>
          </div>
        </Dialogo>
      )}
    </section>
  );
}

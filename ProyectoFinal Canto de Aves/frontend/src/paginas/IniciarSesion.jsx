import { useState } from 'react';
import { Link, Navigate, useNavigate } from 'react-router-dom';
import { iniciarSesion } from '../api/autenticacion.js';
import { ErrorApi } from '../api/cliente.js';
import Alerta from '../componentes/Alerta.jsx';
import Campo from '../componentes/Campo.jsx';
import { paginaInicial, useSesion } from '../contexto/ContextoSesion.jsx';

export default function IniciarSesion() {
  const { usuario, entrar } = useSesion();
  const navegar = useNavigate();
  const [correo, setCorreo] = useState('');
  const [clave, setClave] = useState('');
  const [enviando, setEnviando] = useState(false);
  const [error, setError] = useState('');

  if (usuario) {
    return <Navigate to={paginaInicial(usuario)} replace />;
  }

  async function enviar(evento) {
    evento.preventDefault();
    setEnviando(true);
    setError('');
    try {
      const respuesta = await iniciarSesion({ correo, clave });
      entrar(respuesta);
      navegar(paginaInicial(respuesta.usuario), { replace: true });
    } catch (problema) {
      const credenciales = problema instanceof ErrorApi && problema.estado === 401;
      setError(credenciales ? 'Correo o clave incorrectos, o la cuenta está desactivada.' : problema.message);
      setEnviando(false);
    }
  }

  return (
    <section className="formulario-corto">
      <h1>Iniciar sesión</h1>
      <form className="tarjeta" onSubmit={enviar}>
        <Campo etiqueta="Correo">
          <input type="email" required autoComplete="email" value={correo} onChange={(e) => setCorreo(e.target.value)} />
        </Campo>
        <Campo etiqueta="Clave">
          <input type="password" required autoComplete="current-password" value={clave} onChange={(e) => setClave(e.target.value)} />
        </Campo>
        <Alerta>{error}</Alerta>
        <button type="submit" className="boton boton-ancho" disabled={enviando}>
          {enviando ? 'Entrando…' : 'Entrar'}
        </button>
      </form>
      <p className="centrado">¿Aún no tienes cuenta? <Link to="/crear-cuenta">Crea una</Link></p>
    </section>
  );
}

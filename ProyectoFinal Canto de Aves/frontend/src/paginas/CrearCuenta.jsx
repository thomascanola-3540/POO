import { useState } from 'react';
import { Link, Navigate, useNavigate } from 'react-router-dom';
import { registrarUsuario } from '../api/autenticacion.js';
import Alerta from '../componentes/Alerta.jsx';
import Campo from '../componentes/Campo.jsx';
import { paginaInicial, useSesion } from '../contexto/ContextoSesion.jsx';

// RF01: el colaborador anónimo se convierte en colaborador registrado.
export default function CrearCuenta() {
  const { usuario, entrar } = useSesion();
  const navegar = useNavigate();
  const [datos, setDatos] = useState({ nombres: '', apellidos: '', correo: '', clave: '' });
  const [enviando, setEnviando] = useState(false);
  const [error, setError] = useState('');

  if (usuario) {
    return <Navigate to={paginaInicial(usuario)} replace />;
  }

  const cambiar = (campo) => (e) => setDatos({ ...datos, [campo]: e.target.value });

  async function enviar(evento) {
    evento.preventDefault();
    setEnviando(true);
    setError('');
    try {
      const respuesta = await registrarUsuario(datos);
      entrar(respuesta);
      navegar(paginaInicial(respuesta.usuario), { replace: true });
    } catch (problema) {
      setError(problema.message);
      setEnviando(false);
    }
  }

  return (
    <section className="formulario-corto">
      <h1>Crear cuenta</h1>
      <p className="texto-suave">Con una cuenta tienes un perfil con tus aportes y puedes escuchar los de la comunidad.</p>
      <form className="tarjeta" onSubmit={enviar}>
        <Campo etiqueta="Nombres">
          <input required maxLength={100} autoComplete="given-name" value={datos.nombres} onChange={cambiar('nombres')} />
        </Campo>
        <Campo etiqueta="Apellidos">
          <input required maxLength={100} autoComplete="family-name" value={datos.apellidos} onChange={cambiar('apellidos')} />
        </Campo>
        <Campo etiqueta="Correo">
          <input type="email" required autoComplete="email" value={datos.correo} onChange={cambiar('correo')} />
        </Campo>
        <Campo etiqueta="Clave" ayuda="Mínimo 8 caracteres.">
          <input type="password" required minLength={8} autoComplete="new-password" value={datos.clave} onChange={cambiar('clave')} />
        </Campo>
        <Alerta>{error}</Alerta>
        <button type="submit" className="boton boton-ancho" disabled={enviando}>
          {enviando ? 'Creando…' : 'Crear cuenta'}
        </button>
      </form>
      <p className="centrado">¿Ya tienes cuenta? <Link to="/iniciar-sesion">Inicia sesión</Link></p>
    </section>
  );
}

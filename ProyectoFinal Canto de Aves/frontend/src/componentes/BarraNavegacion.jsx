import { Link, NavLink, useNavigate } from 'react-router-dom';
import { useSesion } from '../contexto/ContextoSesion.jsx';

export default function BarraNavegacion() {
  const { usuario, esAdministrador, esColaborador, salir } = useSesion();
  const navegar = useNavigate();

  function cerrarSesion() {
    salir();
    navegar('/');
  }

  return (
    <header className="barra">
      <div className="contenedor barra-contenido">
        <Link to="/" className="marca">
          <svg viewBox="0 0 24 24" aria-hidden="true" className="marca-icono">
            <path d="M3 10v4M7 7v10M11 4v16M15 8v8M19 6v12M22 11v2" />
          </svg>
          Canto de Aves
        </Link>

        <nav className="menu" aria-label="Principal">
          {!esAdministrador && <NavLink to="/registrar-evento">Registrar evento</NavLink>}
          {esColaborador && <NavLink to="/comunidad">Comunidad</NavLink>}
          {esColaborador && <NavLink to="/perfil">Mi perfil</NavLink>}
          {esAdministrador && <NavLink to="/admin/eventos">Eventos</NavLink>}
          {esAdministrador && <NavLink to="/admin/catalogos">Catálogos</NavLink>}
          {esAdministrador && <NavLink to="/admin/usuarios">Usuarios y roles</NavLink>}
          {!usuario && <NavLink to="/iniciar-sesion">Iniciar sesión</NavLink>}
          {!usuario && <NavLink to="/crear-cuenta">Crear cuenta</NavLink>}
        </nav>

        {usuario && (
          <div className="barra-usuario">
            <span>{usuario.nombres}</span>
            <button type="button" className="boton boton-claro boton-pequeno" onClick={cerrarSesion}>Salir</button>
          </div>
        )}
      </div>
    </header>
  );
}

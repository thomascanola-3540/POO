import { Navigate } from 'react-router-dom';
import { paginaInicial, useSesion } from '../contexto/ContextoSesion.jsx';

// Solo mejora la navegación: quien de verdad protege los datos es el backend con [Authorize].
export default function RutaProtegida({ roles, children }) {
  const { usuario } = useSesion();

  if (!usuario) {
    return <Navigate to="/iniciar-sesion" replace />;
  }
  if (!roles.includes(usuario.rol)) {
    return <Navigate to={paginaInicial(usuario)} replace />;
  }
  return children;
}

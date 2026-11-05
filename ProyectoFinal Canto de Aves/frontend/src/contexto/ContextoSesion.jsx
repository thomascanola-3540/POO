import { createContext, useCallback, useContext, useEffect, useMemo, useState } from 'react';
import { EVENTO_SESION_EXPIRADA } from '../api/cliente.js';
import { ROLES } from '../utilidades/roles.js';
import { borrarSesion, guardarSesion, leerSesionGuardada, tokenVencido } from '../utilidades/sesionGuardada.js';

const ContextoSesion = createContext(null);

function sesionInicial() {
  const sesion = leerSesionGuardada();
  if (sesion && tokenVencido(sesion.token)) {
    borrarSesion();
    return null;
  }
  return sesion;
}

// Comparte con toda la aplicación quién inició sesión y con qué rol.
export function ProveedorSesion({ children }) {
  const [sesion, setSesion] = useState(sesionInicial);

  useEffect(() => {
    const alExpirar = () => setSesion(null);
    window.addEventListener(EVENTO_SESION_EXPIRADA, alExpirar);
    return () => window.removeEventListener(EVENTO_SESION_EXPIRADA, alExpirar);
  }, []);

  const entrar = useCallback((respuesta) => {
    guardarSesion(respuesta);
    setSesion(respuesta);
  }, []);

  const salir = useCallback(() => {
    borrarSesion();
    setSesion(null);
  }, []);

  const actualizarUsuario = useCallback((usuario) => {
    setSesion((actual) => {
      const nueva = { ...actual, usuario };
      guardarSesion(nueva);
      return nueva;
    });
  }, []);

  const valor = useMemo(() => ({
    usuario: sesion?.usuario ?? null,
    esAdministrador: sesion?.usuario.rol === ROLES.ADMINISTRADOR,
    esColaborador: sesion?.usuario.rol === ROLES.COLABORADOR,
    entrar,
    salir,
    actualizarUsuario,
  }), [sesion, entrar, salir, actualizarUsuario]);

  return <ContextoSesion.Provider value={valor}>{children}</ContextoSesion.Provider>;
}

export function useSesion() {
  return useContext(ContextoSesion);
}

// Página a la que entra cada rol después de iniciar sesión.
export function paginaInicial(usuario) {
  if (usuario.rol === ROLES.ADMINISTRADOR) return '/admin/eventos';
  if (usuario.rol === ROLES.COLABORADOR) return '/comunidad';
  return '/';
}

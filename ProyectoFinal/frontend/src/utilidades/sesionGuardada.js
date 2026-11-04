// La sesión se guarda en sessionStorage: dura mientras la pestaña esté abierta.
const CLAVE = 'canto-aves-sesion';

export function leerSesionGuardada() {
  try {
    return JSON.parse(sessionStorage.getItem(CLAVE));
  } catch {
    return null;
  }
}

export function guardarSesion(sesion) {
  sessionStorage.setItem(CLAVE, JSON.stringify(sesion));
}

export function borrarSesion() {
  sessionStorage.removeItem(CLAVE);
}

// Lee la fecha de vencimiento que viene dentro del JWT.
export function tokenVencido(token) {
  try {
    const parte = token.split('.')[1].replace(/-/g, '+').replace(/_/g, '/');
    const { exp } = JSON.parse(atob(parte));
    return exp * 1000 <= Date.now();
  } catch {
    return true;
  }
}

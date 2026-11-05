import { borrarSesion, leerSesionGuardada, tokenVencido } from '../utilidades/sesionGuardada.js';

export const URL_API = import.meta.env.VITE_API_URL ?? 'http://localhost:5299';
export const EVENTO_SESION_EXPIRADA = 'sesion-expirada';

export class ErrorApi extends Error {
  constructor(mensaje, estado) {
    super(mensaje);
    this.estado = estado;
  }
}

function cerrarSesionExpirada() {
  borrarSesion();
  window.dispatchEvent(new Event(EVENTO_SESION_EXPIRADA));
}

// El backend responde texto en las reglas de negocio y JSON en las validaciones de los DTO.
async function leerMensajeDeError(respuesta) {
  if (respuesta.status === 401) return 'Tu sesión no es válida. Inicia sesión de nuevo.';
  if (respuesta.status === 403) return 'No tienes permiso para realizar esta acción.';
  if (respuesta.status === 404) return 'No se encontró lo que buscabas.';

  const texto = await respuesta.text();
  try {
    const datos = JSON.parse(texto);
    return datos.errors ? Object.values(datos.errors).flat().join(' ') : datos.title ?? texto;
  } catch {
    return texto || 'Ocurrió un error inesperado.';
  }
}

// Une los parámetros a la ruta y omite los que están vacíos: conParametros('/x', { id: 1 }) -> '/x?id=1'
export function conParametros(ruta, parametros) {
  const consulta = new URLSearchParams();
  Object.entries(parametros).forEach(([nombre, valor]) => {
    if (valor !== null && valor !== undefined && valor !== '') {
      consulta.append(nombre, valor);
    }
  });
  const texto = consulta.toString();
  return texto ? `${ruta}?${texto}` : ruta;
}

// Punto único por donde pasan todas las peticiones: agrega el token y traduce los errores.
export async function pedir(ruta, { metodo = 'GET', cuerpo, formulario, comoArchivo = false } = {}) {
  const sesion = leerSesionGuardada();
  const cabeceras = {};

  if (sesion) {
    // Sin esto, un evento enviado con el token vencido se guardaría como anónimo sin avisar.
    if (tokenVencido(sesion.token)) {
      cerrarSesionExpirada();
      throw new ErrorApi('Tu sesión expiró. Inicia sesión de nuevo.', 401);
    }
    cabeceras.Authorization = `Bearer ${sesion.token}`;
  }
  if (cuerpo) {
    cabeceras['Content-Type'] = 'application/json';
  }

  let respuesta;
  try {
    respuesta = await fetch(URL_API + ruta, {
      method: metodo,
      headers: cabeceras,
      body: formulario ?? (cuerpo ? JSON.stringify(cuerpo) : undefined),
    });
  } catch {
    throw new ErrorApi('No se pudo conectar con el servidor. Revisa que la API esté encendida.', 0);
  }

  if (!respuesta.ok) {
    if (respuesta.status === 401 && sesion) {
      cerrarSesionExpirada();
    }
    throw new ErrorApi(await leerMensajeDeError(respuesta), respuesta.status);
  }

  if (comoArchivo) return respuesta.blob();
  if (respuesta.status === 204) return null;
  return respuesta.json();
}

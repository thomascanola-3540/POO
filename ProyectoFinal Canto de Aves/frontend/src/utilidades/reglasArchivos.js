// Deben coincidir con Modelos/ConfiguracionArchivos.cs (RN10).
// El backend vuelve a validar; esto solo evita enviar un archivo que será rechazado.
const MEGABYTE = 1024 * 1024;

export const REGLAS_AUDIO = { extensiones: ['.mp3', '.wav', '.ogg', '.m4a'], maximoBytes: 15 * MEGABYTE };
export const REGLAS_IMAGEN = { extensiones: ['.jpg', '.jpeg', '.png'], maximoBytes: 2 * MEGABYTE };
export const MAXIMO_IMAGENES = 5;

// Devuelve el problema del archivo, o null si cumple las reglas.
export function problemaDeArchivo(archivo, reglas) {
  const nombre = archivo.name.toLowerCase();
  if (!reglas.extensiones.some((extension) => nombre.endsWith(extension))) {
    return `"${archivo.name}" no es válido. Se aceptan: ${reglas.extensiones.join(', ')}.`;
  }
  if (archivo.size === 0) {
    return `"${archivo.name}" está vacío.`;
  }
  if (archivo.size > reglas.maximoBytes) {
    return `"${archivo.name}" supera el máximo de ${reglas.maximoBytes / MEGABYTE} MB.`;
  }
  return null;
}

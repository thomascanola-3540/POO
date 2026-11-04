import { pedir } from './cliente.js';

// La ruta la entrega el backend en cada archivo. Se pide con fetch porque necesita el token:
// una etiqueta <audio src="..."> no puede enviar la cabecera Authorization.
export const descargarArchivo = (ruta) => pedir(ruta, { comoArchivo: true });

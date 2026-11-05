import { conParametros, pedir } from './cliente.js';

export const obtenerAves = () => pedir('/api/Ave/GetAves');
export const obtenerTodasLasAves = () => pedir('/api/Ave/GetTodasLasAves');
export const crearAve = (datos) => pedir('/api/Ave/AgregarAve', { metodo: 'POST', cuerpo: datos });
export const actualizarAve = (id, datos) => pedir(conParametros('/api/Ave/ActualizarAve', { id }), { metodo: 'PUT', cuerpo: datos });
export const cambiarEstadoAve = (id, activa) => pedir(conParametros('/api/Ave/CambiarEstado', { id, activa }), { metodo: 'PUT' });

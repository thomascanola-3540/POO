import { conParametros, pedir } from './cliente.js';

export const obtenerCiudadesPorDepartamento = (departamentoId) => pedir(conParametros('/api/Ciudad/GetCiudadesPorDepartamento', { departamentoId }));
export const obtenerTodasLasCiudades = () => pedir('/api/Ciudad/GetTodasLasCiudades');
export const crearCiudad = (datos) => pedir('/api/Ciudad/AgregarCiudad', { metodo: 'POST', cuerpo: datos });
export const actualizarCiudad = (id, datos) => pedir(conParametros('/api/Ciudad/ActualizarCiudad', { id }), { metodo: 'PUT', cuerpo: datos });
export const cambiarEstadoCiudad = (id, activo) => pedir(conParametros('/api/Ciudad/CambiarEstado', { id, activo }), { metodo: 'PUT' });

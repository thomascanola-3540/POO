import { conParametros, pedir } from './cliente.js';

export const obtenerRoles = () => pedir('/api/Rol/GetRoles');
export const crearRol = (datos) => pedir('/api/Rol/AgregarRol', { metodo: 'POST', cuerpo: datos });
export const actualizarRol = (id, datos) => pedir(conParametros('/api/Rol/ActualizarRol', { id }), { metodo: 'PUT', cuerpo: datos });

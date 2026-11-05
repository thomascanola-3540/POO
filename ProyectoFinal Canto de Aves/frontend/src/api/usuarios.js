import { conParametros, pedir } from './cliente.js';

export const obtenerUsuarios = () => pedir('/api/Usuario/GetUsuarios');
export const cambiarRolUsuario = (id, rolId) => pedir(conParametros('/api/Usuario/CambiarRol', { id, rolId }), { metodo: 'PUT' });
export const cambiarEstadoUsuario = (id, activo) => pedir(conParametros('/api/Usuario/CambiarEstado', { id, activo }), { metodo: 'PUT' });

import { conParametros, pedir } from './cliente.js';

export const obtenerPerfil = () => pedir('/api/Perfil/GetPerfil');
export const actualizarPerfil = (datos) => pedir('/api/Perfil/ActualizarPerfil', { metodo: 'PUT', cuerpo: datos });
export const obtenerMisEventos = () => pedir('/api/Perfil/GetMisEventos');
export const desvincularEvento = (id) => pedir(conParametros('/api/Perfil/DesvincularEvento', { id }), { metodo: 'PUT' });

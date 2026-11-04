import { pedir } from './cliente.js';

export const registrarUsuario = (datos) => pedir('/api/Auth/Registro', { metodo: 'POST', cuerpo: datos });
export const iniciarSesion = (datos) => pedir('/api/Auth/Login', { metodo: 'POST', cuerpo: datos });

import { conParametros, pedir } from './cliente.js';

export const obtenerDepartamentos = () => pedir('/api/Departamento/GetDepartamentos');
export const obtenerTodosLosDepartamentos = () => pedir('/api/Departamento/GetTodosLosDepartamentos');
export const crearDepartamento = (datos) => pedir('/api/Departamento/AgregarDepartamento', { metodo: 'POST', cuerpo: datos });
export const actualizarDepartamento = (id, datos) => pedir(conParametros('/api/Departamento/ActualizarDepartamento', { id }), { metodo: 'PUT', cuerpo: datos });
export const cambiarEstadoDepartamento = (id, activo) => pedir(conParametros('/api/Departamento/CambiarEstado', { id, activo }), { metodo: 'PUT' });

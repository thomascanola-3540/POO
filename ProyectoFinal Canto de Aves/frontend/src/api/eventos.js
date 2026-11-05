import { conParametros, pedir } from './cliente.js';

export const registrarEvento = (formulario) => pedir('/api/Evento/RegistrarEvento', { metodo: 'POST', formulario });
export const obtenerEventos = () => pedir('/api/Evento/GetEventos');
export const filtrarEventos = (filtro) => pedir(conParametros('/api/Evento/FiltrarEventos', filtro));
export const validarEvento = (id, aprobado, aveId) => pedir(conParametros('/api/Evento/ValidarEvento', { id, aprobado, aveId }), { metodo: 'PUT' });
export const exportarDataset = (filtro) => pedir(conParametros('/api/Evento/ExportarDataset', filtro), { comoArchivo: true });

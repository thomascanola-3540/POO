import { useCallback } from 'react';
import { actualizarRol, crearRol, obtenerRoles } from '../api/roles.js';
import { ROLES_DEL_SISTEMA } from '../utilidades/roles.js';
import GestorCatalogo from './GestorCatalogo.jsx';

const CAMPOS = [
  {
    nombre: 'nombre',
    etiqueta: 'Nombre',
    longitud: 50,
    valorDe: (rol) => rol.nombre,
    // Los permisos de la API dependen de estos nombres.
    bloqueado: (rol) => ROLES_DEL_SISTEMA.includes(rol.nombre),
  },
  { nombre: 'descripcion', etiqueta: 'Descripción', longitud: 250, valorDe: (rol) => rol.descripcion },
];

const COLUMNAS = [
  { titulo: 'Nombre', valor: (rol) => rol.nombre },
  { titulo: 'Descripción', valor: (rol) => rol.descripcion },
];

export default function CatalogoRoles({ alCambiar }) {
  const cargar = useCallback(() => obtenerRoles(), []);

  const crear = async (datos) => {
    await crearRol(datos);
    alCambiar();
  };
  const actualizar = async (id, datos) => {
    await actualizarRol(id, datos);
    alCambiar();
  };

  return (
    <GestorCatalogo
      titulo="Roles"
      descripcion="Los permisos de la API están definidos para los tres roles del sistema; un rol nuevo no tiene acceso a las funciones protegidas."
      campos={CAMPOS}
      columnas={COLUMNAS}
      cargar={cargar}
      crear={crear}
      actualizar={actualizar}
    />
  );
}

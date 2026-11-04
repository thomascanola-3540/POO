import { useCallback } from 'react';
import { actualizarDepartamento, cambiarEstadoDepartamento, crearDepartamento, obtenerTodosLosDepartamentos } from '../api/departamentos.js';
import GestorCatalogo from './GestorCatalogo.jsx';

const CAMPOS = [{ nombre: 'nombre', etiqueta: 'Nombre', longitud: 100, valorDe: (departamento) => departamento.nombre }];
const COLUMNAS = [{ titulo: 'Nombre', valor: (departamento) => departamento.nombre }];

export default function CatalogoDepartamentos() {
  const cargar = useCallback(() => obtenerTodosLosDepartamentos(), []);
  return (
    <GestorCatalogo
      titulo="Departamentos"
      descripcion="Al desactivar un departamento, sus ciudades dejan de ofrecerse en el formulario de eventos."
      campos={CAMPOS}
      columnas={COLUMNAS}
      cargar={cargar}
      crear={crearDepartamento}
      actualizar={actualizarDepartamento}
      cambiarEstado={cambiarEstadoDepartamento}
      estaActivo={(departamento) => departamento.activo}
    />
  );
}

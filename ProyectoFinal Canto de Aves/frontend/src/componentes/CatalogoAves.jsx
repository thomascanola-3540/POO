import { useCallback } from 'react';
import { actualizarAve, cambiarEstadoAve, crearAve, obtenerTodasLasAves } from '../api/aves.js';
import GestorCatalogo from './GestorCatalogo.jsx';

const CAMPOS = [
  { nombre: 'nombreComun', etiqueta: 'Nombre común', valorDe: (ave) => ave.nombreComun },
  { nombre: 'nombreCientifico', etiqueta: 'Nombre científico', valorDe: (ave) => ave.nombreCientifico },
  { nombre: 'descripcion', etiqueta: 'Descripción', opcional: true, longitud: 1000, valorDe: (ave) => ave.descripcion },
];

const COLUMNAS = [
  { titulo: 'Nombre común', valor: (ave) => ave.nombreComun },
  { titulo: 'Nombre científico', valor: (ave) => <em>{ave.nombreCientifico}</em> },
  { titulo: 'Descripción', valor: (ave) => ave.descripcion ?? '—' },
];

export default function CatalogoAves() {
  const cargar = useCallback(() => obtenerTodasLasAves(), []);
  return (
    <GestorCatalogo
      titulo="Aves"
      descripcion="Especies con las que se identifican los eventos."
      campos={CAMPOS}
      columnas={COLUMNAS}
      cargar={cargar}
      crear={crearAve}
      actualizar={actualizarAve}
      cambiarEstado={cambiarEstadoAve}
      estaActivo={(ave) => ave.activa}
    />
  );
}

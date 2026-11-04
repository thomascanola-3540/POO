import { useCallback, useMemo } from 'react';
import { actualizarCiudad, cambiarEstadoCiudad, crearCiudad, obtenerTodasLasCiudades } from '../api/ciudades.js';
import { obtenerDepartamentos } from '../api/departamentos.js';
import { useDatos } from '../hooks/useDatos.js';
import GestorCatalogo from './GestorCatalogo.jsx';

const COLUMNAS = [
  { titulo: 'Nombre', valor: (ciudad) => ciudad.nombre },
  { titulo: 'Departamento', valor: (ciudad) => ciudad.departamento.nombre },
];

// El backend espera DepartamentoId como número, y el formulario lo entrega como texto.
const convertir = (datos) => ({ ...datos, departamentoId: Number(datos.departamentoId) });

export default function CatalogoCiudades() {
  const cargar = useCallback(() => obtenerTodasLasCiudades(), []);
  const cargarDepartamentos = useCallback(() => obtenerDepartamentos(), []);
  const { datos: departamentos } = useDatos(cargarDepartamentos);

  const campos = useMemo(() => [
    { nombre: 'nombre', etiqueta: 'Nombre', longitud: 100, valorDe: (ciudad) => ciudad.nombre },
    {
      nombre: 'departamentoId',
      etiqueta: 'Departamento',
      valorDe: (ciudad) => String(ciudad.departamento.id),
      opciones: (departamentos ?? []).map((d) => ({ valor: String(d.id), texto: d.nombre })),
    },
  ], [departamentos]);

  return (
    <GestorCatalogo
      titulo="Ciudades"
      descripcion="Cada ciudad pertenece a un único departamento."
      campos={campos}
      columnas={COLUMNAS}
      cargar={cargar}
      crear={(datos) => crearCiudad(convertir(datos))}
      actualizar={(id, datos) => actualizarCiudad(id, convertir(datos))}
      cambiarEstado={cambiarEstadoCiudad}
      estaActivo={(ciudad) => ciudad.activo}
    />
  );
}

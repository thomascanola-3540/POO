import { useState } from 'react';
import CatalogoAves from '../componentes/CatalogoAves.jsx';
import CatalogoCiudades from '../componentes/CatalogoCiudades.jsx';
import CatalogoDepartamentos from '../componentes/CatalogoDepartamentos.jsx';

const PESTANAS = [
  { id: 'aves', titulo: 'Aves', componente: CatalogoAves },
  { id: 'departamentos', titulo: 'Departamentos', componente: CatalogoDepartamentos },
  { id: 'ciudades', titulo: 'Ciudades', componente: CatalogoCiudades },
];

// RF10 y RF11: parametrización de catálogos (RN06).
export default function AdminCatalogos() {
  const [activa, setActiva] = useState(PESTANAS[0].id);
  const Seccion = PESTANAS.find((pestana) => pestana.id === activa).componente;

  return (
    <section>
      <h1>Catálogos</h1>
      <div className="pestanas" role="group" aria-label="Catálogo">
        {PESTANAS.map((pestana) => (
          <button
            key={pestana.id}
            type="button"
            aria-pressed={pestana.id === activa}
            className={pestana.id === activa ? 'pestana pestana-activa' : 'pestana'}
            onClick={() => setActiva(pestana.id)}
          >
            {pestana.titulo}
          </button>
        ))}
      </div>
      <Seccion />
    </section>
  );
}

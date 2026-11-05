import { useCallback } from 'react';
import { obtenerEventos } from '../api/eventos.js';
import EstadoCarga from '../componentes/EstadoCarga.jsx';
import ListaEventos from '../componentes/ListaEventos.jsx';
import { useDatos } from '../hooks/useDatos.js';

// RF08 y RF09: consultar y escuchar los eventos, sin poder modificarlos (RN05).
export default function Comunidad() {
  const cargar = useCallback(() => obtenerEventos(), []);
  const { datos: eventos, cargando, error, recargar } = useDatos(cargar);

  return (
    <section>
      <h1>Eventos de la comunidad</h1>
      <p className="texto-suave">Escucha lo que han registrado otros colaboradores en todo el país.</p>
      <EstadoCarga cargando={cargando} error={error} reintentar={recargar}>
        <ListaEventos eventos={eventos ?? []} mensajeVacio="Todavía no hay eventos registrados. ¡Puedes ser el primero!" />
      </EstadoCarga>
    </section>
  );
}

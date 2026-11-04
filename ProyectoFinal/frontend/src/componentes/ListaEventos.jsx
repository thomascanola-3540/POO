import { useState } from 'react';
import TarjetaEvento from './TarjetaEvento.jsx';

const POR_TANDA = 12;

// El backend entrega todos los eventos; aquí se van mostrando por tandas para no cargar todo de una vez.
export default function ListaEventos({ eventos, mensajeVacio, acciones }) {
  const [visibles, setVisibles] = useState(POR_TANDA);

  if (eventos.length === 0) {
    return <div className="panel-vacio"><p>{mensajeVacio}</p></div>;
  }

  return (
    <>
      <div className="rejilla-eventos">
        {eventos.slice(0, visibles).map((evento) => (
          <TarjetaEvento key={evento.id} evento={evento}>{acciones?.(evento)}</TarjetaEvento>
        ))}
      </div>
      {visibles < eventos.length && (
        <div className="centrado">
          <button type="button" className="boton boton-secundario" onClick={() => setVisibles(visibles + POR_TANDA)}>
            Ver más ({eventos.length - visibles} restantes)
          </button>
        </div>
      )}
    </>
  );
}

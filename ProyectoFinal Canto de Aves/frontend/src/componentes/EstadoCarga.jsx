import Alerta from './Alerta.jsx';

// Muestra "cargando" o el error de una consulta; si todo salió bien, muestra su contenido.
export default function EstadoCarga({ cargando, error, reintentar, children }) {
  if (error) {
    return (
      <div className="panel-vacio">
        <Alerta>{error}</Alerta>
        {reintentar && <button type="button" className="boton boton-secundario" onClick={reintentar}>Reintentar</button>}
      </div>
    );
  }
  if (cargando) {
    return <p className="cargando" role="status">Cargando…</p>;
  }
  return children;
}

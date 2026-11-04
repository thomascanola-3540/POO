import { formatearFecha } from '../utilidades/formato.js';
import EstadoValidacion from './EstadoValidacion.jsx';
import ImagenEvento from './ImagenEvento.jsx';
import ReproductorAudio from './ReproductorAudio.jsx';

// Muestra un evento. Las acciones (validar, desvincular) las decide la página que la usa.
export default function TarjetaEvento({ evento, children }) {
  const audios = evento.archivos.filter((archivo) => archivo.tipo === 'Audio');
  const imagenes = evento.archivos.filter((archivo) => archivo.tipo === 'Imagen');

  return (
    <article className="tarjeta evento">
      <header className="evento-cabecera">
        <div>
          <h3>{evento.aveIdentificada ?? evento.nombreHipotetico ?? 'Ave sin identificar'}</h3>
          <p className="texto-suave">
            {!evento.aveIdentificada && evento.nombreHipotetico ? 'Sugerida por el colaborador · ' : ''}
            {formatearFecha(evento.fechaHora)}
          </p>
        </div>
        <EstadoValidacion estado={evento.estadoValidacion} />
      </header>

      {audios.map((archivo) => <ReproductorAudio key={archivo.id} archivo={archivo} />)}

      {imagenes.length > 0 && (
        <div className="evento-imagenes">
          {imagenes.map((archivo) => <ImagenEvento key={archivo.id} archivo={archivo} />)}
        </div>
      )}

      <dl className="evento-datos">
        <dt>Lugar</dt>
        <dd>{evento.ciudad}, {evento.departamento}</dd>
        {evento.direccion && <><dt>Dirección</dt><dd>{evento.direccion}</dd></>}
        {evento.descripcionLugar && <><dt>Descripción</dt><dd>{evento.descripcionLugar}</dd></>}
        <dt>Coordenadas</dt>
        <dd>{evento.latitud}, {evento.longitud}</dd>
        <dt>Registró</dt>
        <dd>{evento.colaborador}</dd>
        {evento.notas && <><dt>Notas</dt><dd>{evento.notas}</dd></>}
      </dl>

      {children && <div className="evento-acciones">{children}</div>}
    </article>
  );
}

import { Link } from 'react-router-dom';
import { useSesion } from '../contexto/ContextoSesion.jsx';

// Alturas de las barras que dibujan la onda de sonido de la portada.
const ONDA = [18, 34, 58, 30, 72, 96, 54, 28, 44, 82, 62, 36, 20, 48, 88, 66, 40, 24, 52, 76, 42, 26, 60, 34, 16];

export default function Inicio() {
  const { esAdministrador } = useSesion();

  return (
    <>
      <section className="portada">
        <div className="portada-texto">
          <p className="portada-antetitulo">Ciencia ciudadana para la avifauna de Colombia</p>
          <h1>Cada canto que grabas ayuda a conocer mejor a nuestras aves.</h1>
          <p>
            Colombia tiene cerca de 1.900 especies registradas y muchas se detectan más fácil por su canto que a
            simple vista. Sube tu grabación con el lugar y el momento, y ayúdanos a construir un gran dataset para la
            investigación.
          </p>
          <div className="fila-botones">
            {esAdministrador ? (
              <Link to="/admin/eventos" className="boton boton-acento">Ir a los eventos</Link>
            ) : (
              <Link to="/registrar-evento" className="boton boton-acento">Registrar un canto</Link>
            )}
            <span className="portada-nota">No necesitas una cuenta para aportar.</span>
          </div>
        </div>
        <div className="onda" aria-hidden="true">
          {ONDA.map((altura, indice) => <span key={indice} style={{ height: `${altura}%` }} />)}
        </div>
      </section>

      <section className="pasos" aria-labelledby="titulo-pasos">
        <h2 id="titulo-pasos">Así funciona</h2>
        <ol>
          <li>
            <h3>Graba o elige tu audio</h3>
            <p>El audio es lo único que debes adjuntar. También puedes agregar fotos del ave o del lugar.</p>
          </li>
          <li>
            <h3>Cuenta dónde y cuándo</h3>
            <p>Indica la fecha, la ciudad y las coordenadas. Si crees saber qué ave es, sugiere su nombre.</p>
          </li>
          <li>
            <h3>Un administrador lo valida</h3>
            <p>Tu sugerencia se revisa y el evento queda identificado con una especie del catálogo.</p>
          </li>
        </ol>
      </section>
    </>
  );
}

import { Link } from 'react-router-dom';

export default function NoEncontrada() {
  return (
    <section className="panel-vacio">
      <h1>Esta página no existe</h1>
      <Link to="/" className="boton">Volver al inicio</Link>
    </section>
  );
}

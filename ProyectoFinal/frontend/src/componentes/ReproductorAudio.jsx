import { useEffect, useState } from 'react';
import { descargarArchivo } from '../api/archivos.js';
import { formatearTamano } from '../utilidades/formato.js';
import Alerta from './Alerta.jsx';

// RF09: el audio se descarga solo cuando la persona pide escucharlo.
export default function ReproductorAudio({ archivo }) {
  const [url, setUrl] = useState(null);
  const [cargando, setCargando] = useState(false);
  const [error, setError] = useState('');

  useEffect(() => () => { if (url) URL.revokeObjectURL(url); }, [url]);

  async function escuchar() {
    setCargando(true);
    setError('');
    try {
      setUrl(URL.createObjectURL(await descargarArchivo(archivo.ruta)));
    } catch (problema) {
      setError(problema.message);
    } finally {
      setCargando(false);
    }
  }

  return (
    <div className="audio">
      <div className="audio-datos">
        <span className="audio-nombre">{archivo.nombreOriginal}</span>
        <span className="texto-suave">{formatearTamano(archivo.tamanoBytes)}</span>
      </div>
      {url ? (
        <audio controls autoPlay src={url} aria-label={`Audio ${archivo.nombreOriginal}`} />
      ) : (
        <button type="button" className="boton boton-secundario boton-pequeno" onClick={escuchar} disabled={cargando}>
          {cargando ? 'Cargando…' : '▶ Escuchar'}
        </button>
      )}
      <Alerta>{error}</Alerta>
    </div>
  );
}

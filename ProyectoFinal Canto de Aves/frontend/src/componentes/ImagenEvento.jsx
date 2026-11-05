import { useEffect, useState } from 'react';
import { descargarArchivo } from '../api/archivos.js';

export default function ImagenEvento({ archivo }) {
  const [url, setUrl] = useState(null);
  const [fallo, setFallo] = useState(false);

  useEffect(() => {
    let vigente = true;
    let creada = null;
    descargarArchivo(archivo.ruta)
      .then((contenido) => {
        if (vigente) {
          creada = URL.createObjectURL(contenido);
          setUrl(creada);
        }
      })
      .catch(() => { if (vigente) setFallo(true); });

    return () => {
      vigente = false;
      if (creada) URL.revokeObjectURL(creada);
    };
  }, [archivo.ruta]);

  if (fallo) return <div className="imagen-marco imagen-fallo">Imagen no disponible</div>;
  return (
    <div className="imagen-marco">
      {url && <img src={url} alt={`Registro del evento: ${archivo.nombreOriginal}`} loading="lazy" />}
    </div>
  );
}

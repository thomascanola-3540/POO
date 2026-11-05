import { useCallback, useEffect, useState } from 'react';

// Carga datos de la API y expone su estado. Se vuelve a cargar cuando cambia la función recibida,
// así que quien la usa debe envolverla en useCallback si depende de algún valor.
export function useDatos(cargar) {
  const [datos, setDatos] = useState(null);
  const [cargando, setCargando] = useState(true);
  const [error, setError] = useState('');

  const consultar = useCallback(() => {
    let vigente = true;
    setCargando(true);
    setError('');
    cargar()
      .then((resultado) => { if (vigente) setDatos(resultado); })
      .catch((problema) => { if (vigente) setError(problema.message); })
      .finally(() => { if (vigente) setCargando(false); });
    // Si llega otra consulta antes de que termine esta, se ignora su resultado.
    return () => { vigente = false; };
  }, [cargar]);

  useEffect(() => consultar(), [consultar]);

  return { datos, cargando, error, recargar: consultar };
}

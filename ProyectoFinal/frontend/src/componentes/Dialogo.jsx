import { useEffect, useRef } from 'react';

// Ventana modal con <dialog>: el navegador ya resuelve el foco y la tecla Escape.
export default function Dialogo({ titulo, alCerrar, children }) {
  const referencia = useRef(null);

  useEffect(() => {
    const dialogo = referencia.current;
    dialogo.showModal();
    return () => dialogo.close();
  }, []);

  return (
    <dialog ref={referencia} className="dialogo" onCancel={alCerrar}>
      <h2>{titulo}</h2>
      {children}
    </dialog>
  );
}

import { useCallback, useState } from 'react';
import { obtenerAves } from '../api/aves.js';
import { validarEvento } from '../api/eventos.js';
import { useDatos } from '../hooks/useDatos.js';
import Alerta from './Alerta.jsx';
import Campo from './Campo.jsx';
import Dialogo from './Dialogo.jsx';

// RF14: el administrador confirma o corrige el ave sugerida, o rechaza el evento (RN09).
export default function DialogoValidar({ evento, alCerrar, alValidar }) {
  const cargarAves = useCallback(() => obtenerAves(), []);
  const { datos: aves, error: errorAves } = useDatos(cargarAves);
  const [aveId, setAveId] = useState('');
  const [enviando, setEnviando] = useState(false);
  const [error, setError] = useState('');

  async function decidir(aprobado) {
    setEnviando(true);
    setError('');
    try {
      await validarEvento(evento.id, aprobado, aprobado ? aveId : null);
      alValidar();
    } catch (problema) {
      setError(problema.message);
      setEnviando(false);
    }
  }

  return (
    <Dialogo titulo={`Validar evento #${evento.id}`} alCerrar={alCerrar}>
      <p>
        El colaborador sugirió: <strong>{evento.nombreHipotetico ?? 'ninguna especie'}</strong>.
      </p>
      <Campo etiqueta="Ave identificada">
        <select value={aveId} onChange={(e) => setAveId(e.target.value)}>
          <option value="">Selecciona un ave</option>
          {aves?.map((ave) => (
            <option key={ave.id} value={ave.id}>{ave.nombreComun} ({ave.nombreCientifico})</option>
          ))}
        </select>
      </Campo>
      <Alerta>{error || errorAves}</Alerta>
      <div className="dialogo-acciones">
        <button type="button" className="boton boton-texto" onClick={alCerrar} disabled={enviando}>Cancelar</button>
        <button type="button" className="boton boton-peligro" onClick={() => decidir(false)} disabled={enviando}>Rechazar</button>
        <button type="button" className="boton" onClick={() => decidir(true)} disabled={enviando || !aveId}>Validar</button>
      </div>
    </Dialogo>
  );
}

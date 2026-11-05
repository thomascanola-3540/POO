import { useCallback, useState } from 'react';
import { obtenerTodasLasAves } from '../api/aves.js';
import { obtenerTodasLasCiudades } from '../api/ciudades.js';
import { obtenerTodosLosDepartamentos } from '../api/departamentos.js';
import { exportarDataset, filtrarEventos } from '../api/eventos.js';
import { obtenerUsuarios } from '../api/usuarios.js';
import Alerta from '../componentes/Alerta.jsx';
import Campo from '../componentes/Campo.jsx';
import DialogoValidar from '../componentes/DialogoValidar.jsx';
import EstadoCarga from '../componentes/EstadoCarga.jsx';
import ListaEventos from '../componentes/ListaEventos.jsx';
import { useDatos } from '../hooks/useDatos.js';

const SIN_FILTRO = { departamentoId: '', ciudadId: '', aveId: '', usuarioId: '' };

// RF13 filtrar, RF14 validar y RF15 exportar. Solo para el administrador.
export default function AdminEventos() {
  const [filtro, setFiltro] = useState(SIN_FILTRO);
  const [porValidar, setPorValidar] = useState(null);
  const [exportando, setExportando] = useState(false);
  const [errorExportar, setErrorExportar] = useState('');

  const cargarEventos = useCallback(() => filtrarEventos(filtro), [filtro]);
  const cargarDepartamentos = useCallback(() => obtenerTodosLosDepartamentos(), []);
  const cargarCiudades = useCallback(() => obtenerTodasLasCiudades(), []);
  const cargarAves = useCallback(() => obtenerTodasLasAves(), []);
  const cargarUsuarios = useCallback(() => obtenerUsuarios(), []);

  const { datos: eventos, cargando, error, recargar } = useDatos(cargarEventos);
  const departamentos = useDatos(cargarDepartamentos);
  const ciudades = useDatos(cargarCiudades);
  const aves = useDatos(cargarAves);
  const usuarios = useDatos(cargarUsuarios);

  const ciudadesDelDepartamento = (ciudades.datos ?? []).filter(
    (ciudad) => !filtro.departamentoId || ciudad.departamento.id === Number(filtro.departamentoId),
  );

  async function exportar() {
    setExportando(true);
    setErrorExportar('');
    try {
      const archivo = await exportarDataset(filtro);
      const url = URL.createObjectURL(archivo);
      const enlace = document.createElement('a');
      enlace.href = url;
      enlace.download = 'dataset_aves.csv';
      enlace.click();
      URL.revokeObjectURL(url);
    } catch (problema) {
      setErrorExportar(problema.message);
    } finally {
      setExportando(false);
    }
  }

  return (
    <section>
      <div className="titulo-con-accion">
        <div>
          <h1>Eventos</h1>
          <p className="texto-suave">
            {eventos ? `${eventos.length} ${eventos.length === 1 ? 'evento encontrado' : 'eventos encontrados'}` : 'Consulta, valida y exporta.'}
          </p>
        </div>
        <button type="button" className="boton boton-secundario" onClick={exportar} disabled={exportando}>
          {exportando ? 'Exportando…' : 'Exportar dataset (CSV)'}
        </button>
      </div>
      <Alerta>{errorExportar}</Alerta>

      <form className="tarjeta filtros" aria-label="Filtros de eventos" onSubmit={(e) => e.preventDefault()}>
        <Campo etiqueta="Departamento">
          <select value={filtro.departamentoId} onChange={(e) => setFiltro({ ...filtro, departamentoId: e.target.value, ciudadId: '' })}>
            <option value="">Todos</option>
            {departamentos.datos?.map((d) => <option key={d.id} value={d.id}>{d.nombre}</option>)}
          </select>
        </Campo>
        <Campo etiqueta="Ciudad">
          <select value={filtro.ciudadId} onChange={(e) => setFiltro({ ...filtro, ciudadId: e.target.value })}>
            <option value="">Todas</option>
            {ciudadesDelDepartamento.map((c) => <option key={c.id} value={c.id}>{c.nombre}</option>)}
          </select>
        </Campo>
        <Campo etiqueta="Ave identificada">
          <select value={filtro.aveId} onChange={(e) => setFiltro({ ...filtro, aveId: e.target.value })}>
            <option value="">Todas</option>
            {aves.datos?.map((a) => <option key={a.id} value={a.id}>{a.nombreComun}</option>)}
          </select>
        </Campo>
        <Campo etiqueta="Usuario">
          <select value={filtro.usuarioId} onChange={(e) => setFiltro({ ...filtro, usuarioId: e.target.value })}>
            <option value="">Todos</option>
            {usuarios.datos?.map((u) => <option key={u.id} value={u.id}>{u.nombres} {u.apellidos}</option>)}
          </select>
        </Campo>
        <button type="button" className="boton boton-texto" onClick={() => setFiltro(SIN_FILTRO)}>Limpiar filtros</button>
      </form>

      <EstadoCarga cargando={cargando && !eventos} error={error} reintentar={recargar}>
        <ListaEventos
          eventos={eventos ?? []}
          mensajeVacio="No hay eventos con estos filtros."
          acciones={(evento) => (
            <button type="button" className="boton boton-pequeno" onClick={() => setPorValidar(evento)}>
              {evento.estadoValidacion === 'Pendiente' ? 'Validar' : 'Cambiar validación'}
            </button>
          )}
        />
      </EstadoCarga>

      {porValidar && (
        <DialogoValidar
          evento={porValidar}
          alCerrar={() => setPorValidar(null)}
          alValidar={() => {
            setPorValidar(null);
            recargar();
          }}
        />
      )}
    </section>
  );
}

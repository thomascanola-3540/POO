import { useCallback, useState } from 'react';
import { obtenerRoles } from '../api/roles.js';
import { cambiarEstadoUsuario, cambiarRolUsuario, obtenerUsuarios } from '../api/usuarios.js';
import Alerta from '../componentes/Alerta.jsx';
import CatalogoRoles from '../componentes/CatalogoRoles.jsx';
import EstadoCarga from '../componentes/EstadoCarga.jsx';
import { useSesion } from '../contexto/ContextoSesion.jsx';
import { useDatos } from '../hooks/useDatos.js';
import { ROLES } from '../utilidades/roles.js';

// RF12: consultar usuarios, asignar roles y activar o desactivar cuentas.
export default function AdminUsuarios() {
  const { usuario: actual } = useSesion();
  const [errorAccion, setErrorAccion] = useState('');

  const cargarUsuarios = useCallback(() => obtenerUsuarios(), []);
  const cargarRoles = useCallback(() => obtenerRoles(), []);
  const { datos: usuarios, cargando, error, recargar } = useDatos(cargarUsuarios);
  const roles = useDatos(cargarRoles);

  async function ejecutar(accion) {
    setErrorAccion('');
    try {
      await accion();
      recargar();
    } catch (problema) {
      setErrorAccion(problema.message);
    }
  }

  // El rol anónimo representa a quien no tiene cuenta, así que no se puede asignar.
  const rolesAsignables = (roles.datos ?? []).filter((rol) => rol.nombre !== ROLES.ANONIMO);

  return (
    <>
      <section>
        <h1>Usuarios</h1>
        <p className="texto-suave">
          Si cambias el rol o desactivas una cuenta, el cambio aplica por completo cuando vence su sesión actual (máximo 60 minutos).
        </p>
        <Alerta>{errorAccion}</Alerta>
        <EstadoCarga cargando={cargando && !usuarios} error={error} reintentar={recargar}>
          <div className="tabla-marco">
            <table className="tabla">
              <thead>
                <tr>
                  <th scope="col">Nombre</th>
                  <th scope="col">Correo</th>
                  <th scope="col">Rol</th>
                  <th scope="col">Estado</th>
                  <th scope="col"><span className="solo-lector">Acciones</span></th>
                </tr>
              </thead>
              <tbody>
                {usuarios?.map((usuario) => {
                  const esActual = usuario.id === actual.id;
                  return (
                    <tr key={usuario.id}>
                      <td>{usuario.nombres} {usuario.apellidos}{esActual && <span className="texto-suave"> (tú)</span>}</td>
                      <td>{usuario.correo}</td>
                      <td>
                        <select
                          aria-label={`Rol de ${usuario.nombres}`}
                          disabled={esActual}
                          value={rolesAsignables.find((rol) => rol.nombre === usuario.rol)?.id ?? ''}
                          onChange={(e) => ejecutar(() => cambiarRolUsuario(usuario.id, e.target.value))}
                        >
                          {rolesAsignables.map((rol) => <option key={rol.id} value={rol.id}>{rol.nombre}</option>)}
                        </select>
                      </td>
                      <td>
                        <span className={`insignia ${usuario.activo ? 'insignia-validado' : 'insignia-inactivo'}`}>
                          {usuario.activo ? 'Activo' : 'Inactivo'}
                        </span>
                      </td>
                      <td className="tabla-acciones">
                        <button
                          type="button"
                          className="boton boton-texto boton-pequeno"
                          disabled={esActual}
                          onClick={() => ejecutar(() => cambiarEstadoUsuario(usuario.id, !usuario.activo))}
                        >
                          {usuario.activo ? 'Desactivar' : 'Activar'}
                        </button>
                      </td>
                    </tr>
                  );
                })}
              </tbody>
            </table>
          </div>
        </EstadoCarga>
      </section>

      <CatalogoRoles alCambiar={roles.recargar} />
    </>
  );
}

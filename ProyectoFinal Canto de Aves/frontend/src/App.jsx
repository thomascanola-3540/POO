import { Route, Routes } from 'react-router-dom';
import BarraNavegacion from './componentes/BarraNavegacion.jsx';
import RutaProtegida from './componentes/RutaProtegida.jsx';
import { ROLES } from './utilidades/roles.js';
import AdminCatalogos from './paginas/AdminCatalogos.jsx';
import AdminEventos from './paginas/AdminEventos.jsx';
import AdminUsuarios from './paginas/AdminUsuarios.jsx';
import Comunidad from './paginas/Comunidad.jsx';
import CrearCuenta from './paginas/CrearCuenta.jsx';
import IniciarSesion from './paginas/IniciarSesion.jsx';
import Inicio from './paginas/Inicio.jsx';
import MiPerfil from './paginas/MiPerfil.jsx';
import NoEncontrada from './paginas/NoEncontrada.jsx';
import RegistrarEvento from './paginas/RegistrarEvento.jsx';

const CONSULTAN_EVENTOS = [ROLES.COLABORADOR, ROLES.ADMINISTRADOR];
const SOLO_COLABORADOR = [ROLES.COLABORADOR];
const SOLO_ADMINISTRADOR = [ROLES.ADMINISTRADOR];

export default function App() {
  return (
    <>
      <BarraNavegacion />
      <main className="contenedor">
        <Routes>
          <Route path="/" element={<Inicio />} />
          <Route path="/registrar-evento" element={<RegistrarEvento />} />
          <Route path="/iniciar-sesion" element={<IniciarSesion />} />
          <Route path="/crear-cuenta" element={<CrearCuenta />} />
          <Route path="/comunidad" element={<RutaProtegida roles={CONSULTAN_EVENTOS}><Comunidad /></RutaProtegida>} />
          <Route path="/perfil" element={<RutaProtegida roles={SOLO_COLABORADOR}><MiPerfil /></RutaProtegida>} />
          <Route path="/admin/eventos" element={<RutaProtegida roles={SOLO_ADMINISTRADOR}><AdminEventos /></RutaProtegida>} />
          <Route path="/admin/catalogos" element={<RutaProtegida roles={SOLO_ADMINISTRADOR}><AdminCatalogos /></RutaProtegida>} />
          <Route path="/admin/usuarios" element={<RutaProtegida roles={SOLO_ADMINISTRADOR}><AdminUsuarios /></RutaProtegida>} />
          <Route path="*" element={<NoEncontrada />} />
        </Routes>
      </main>
      <footer className="pie">
        Proyecto de Programación Orientada a Objetos · Universidad Católica de Oriente
      </footer>
    </>
  );
}

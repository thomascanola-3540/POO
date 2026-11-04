using Datos;
using Modelos;
using Modelos.DTOs;
using Servicios.Comandos;
using Servicios.Interfaces;

namespace Servicios.Servicios
{
    public class ServicioEvento : IEvento
    {
        private readonly ICiudad _ciudad;
        private readonly IAve _ave;
        private readonly IUsuario _usuario;
        private readonly IArchivo _archivo;
        private readonly GestorComandos _gestorComandos;

        public ServicioEvento(ICiudad ciudad, IAve ave, IUsuario usuario, IArchivo archivo, GestorComandos gestorComandos)
        {
            _ciudad = ciudad;
            _ave = ave;
            _usuario = usuario;
            _archivo = archivo;
            _gestorComandos = gestorComandos;
        }

        // Caso de uso CU04: primero se valida todo y solo al final se guarda en las listas.
        public Evento RegistrarEvento(EventoDTO eventoDto, ArchivoDTO audioDto, List<ArchivoDTO> imagenesDto, int? idUsuario)
        {
            Ciudad? ciudad = _ciudad.GetCiudadPorId(eventoDto.CiudadId!.Value);
            if (ciudad == null || !ciudad.Activo || !ciudad.Departamento.Activo)
            {
                throw new ExcepcionNegocio("La ciudad no existe o está inactiva.");
            }

            ArchivoAudio audio = _archivo.CrearAudio(audioDto);
            List<ArchivoImagen> imagenes = _archivo.CrearImagenes(imagenesDto);

            Ubicacion ubicacion = new Ubicacion(ContextoDatos.listaUbicaciones.Count + 1, eventoDto.Longitud!.Value,
                eventoDto.Latitud!.Value, eventoDto.Direccion, eventoDto.DescripcionLugar, ciudad);

            // El usuario es opcional: si es null, el evento es de un colaborador anónimo.
            Usuario? usuario = idUsuario != null ? _usuario.GetUsuarioPorId(idUsuario.Value) : null;

            Evento evento = new Evento(ContextoDatos.listaEventos.Count + 1, eventoDto.FechaHora!.Value, ubicacion,
                usuario, eventoDto.Notas, eventoDto.NombreHipotetico);

            _archivo.GuardarArchivo(audio);
            evento.AdjuntarArchivo(audio);
            foreach (ArchivoImagen imagen in imagenes)
            {
                _archivo.GuardarArchivo(imagen);
                evento.AdjuntarArchivo(imagen);
            }

            ContextoDatos.listaUbicaciones.Add(ubicacion);
            ContextoDatos.listaEventos.Add(evento);
            return evento;
        }

        public List<Evento> GetEventos()
        {
            return ContextoDatos.listaEventos.OrderByDescending(item => item.FechaHora).ToList();
        }

        public Evento? GetEventoPorId(int id)
        {
            return ContextoDatos.listaEventos.FirstOrDefault(item => item.Id == id);
        }

        // Los eventos desvinculados ya no aparecen en el perfil del colaborador (RN05).
        public List<Evento> GetEventosDeUsuario(int idUsuario)
        {
            return GetEventos().Where(item => item.PerteneceA(idUsuario) && !item.DesvinculadoDelPerfil).ToList();
        }

        public List<Evento> FiltrarEventos(FiltroEvento filtro)
        {
            return filtro.Aplicar(GetEventos());
        }

        public void ValidarEvento(Evento evento, bool aprobado, int? aveId)
        {
            if (aprobado)
            {
                Ave? ave = aveId != null ? _ave.GetAvePorId(aveId.Value) : null;
                if (ave == null || !ave.Activa)
                {
                    throw new ExcepcionNegocio("Para validar el evento debe indicar un ave activa del catálogo.");
                }
                _gestorComandos.ConfigurarComando(new ComandoValidarEvento(evento, ave));
            }
            else
            {
                _gestorComandos.ConfigurarComando(new ComandoRechazarEvento(evento));
            }

            _gestorComandos.EjecutarComando();
        }

        public void DesvincularEvento(Evento evento)
        {
            _gestorComandos.ConfigurarComando(new ComandoDesvincularEvento(evento));
            _gestorComandos.EjecutarComando();
        }
    }
}

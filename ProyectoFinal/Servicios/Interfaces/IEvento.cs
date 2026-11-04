using Modelos;
using Modelos.DTOs;

namespace Servicios.Interfaces
{
    public interface IEvento
    {
        public Evento RegistrarEvento(EventoDTO eventoDto, ArchivoDTO audioDto, List<ArchivoDTO> imagenesDto, int? idUsuario);
        public List<Evento> GetEventos();
        public Evento? GetEventoPorId(int id);
        public List<Evento> GetEventosDeUsuario(int idUsuario);
        public List<Evento> FiltrarEventos(FiltroEvento filtro);
        public void ValidarEvento(Evento evento, bool aprobado, int? aveId);
        public void DesvincularEvento(Evento evento);
    }
}

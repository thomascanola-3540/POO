namespace Modelos.DTOs
{
    public class EventoRespuestaDTO
    {
        public int Id { get; set; }
        public DateTime FechaHora { get; set; }
        public DateTime FechaRegistro { get; set; }
        public double Longitud { get; set; }
        public double Latitud { get; set; }
        public string? Direccion { get; set; }
        public string? DescripcionLugar { get; set; }
        public string Ciudad { get; set; }
        public string Departamento { get; set; }
        public string? Notas { get; set; }
        public string? NombreHipotetico { get; set; }
        public string EstadoValidacion { get; set; }
        public string? AveIdentificada { get; set; }
        public int? UsuarioId { get; set; }
        public string Colaborador { get; set; }
        public List<ArchivoRespuestaDTO> Archivos { get; set; }

        public EventoRespuestaDTO(Evento evento)
        {
            Id = evento.Id;
            FechaHora = evento.FechaHora;
            FechaRegistro = evento.FechaRegistro;
            Longitud = evento.Ubicacion.Longitud;
            Latitud = evento.Ubicacion.Latitud;
            Direccion = evento.Ubicacion.Direccion;
            DescripcionLugar = evento.Ubicacion.Descripcion;
            Ciudad = evento.Ubicacion.Ciudad.Nombre;
            Departamento = evento.Ubicacion.Ciudad.Departamento.Nombre;
            Notas = evento.Notas;
            NombreHipotetico = evento.NombreHipotetico;
            EstadoValidacion = evento.EstadoValidacion;
            AveIdentificada = evento.AveIdentificada?.NombreComun;
            UsuarioId = evento.Usuario?.Id;
            Colaborador = evento.Usuario != null ? evento.Usuario.NombreCompleto() : "Colaborador anónimo";
            Archivos = evento.Archivos.Select(item => new ArchivoRespuestaDTO(item)).ToList();
        }
    }
}

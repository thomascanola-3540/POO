using System.ComponentModel.DataAnnotations;

namespace Modelos.DTOs
{
    public class EventoDTO
    {
        [Required]
        public DateTime? FechaHora { get; set; }

        [Required]
        public double? Longitud { get; set; }

        [Required]
        public double? Latitud { get; set; }

        [Required]
        public int? CiudadId { get; set; }

        public string? Direccion { get; set; }
        public string? DescripcionLugar { get; set; }
        public string? Notas { get; set; }
        public string? NombreHipotetico { get; set; }
    }
}

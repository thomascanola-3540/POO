using System.ComponentModel.DataAnnotations;

namespace Modelos.DTOs
{
    // Datos del formulario de registro de un evento (RF04).
    // [Required] hace obligatorios los datos que exige la regla RN01.
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

        // Datos opcionales (RN02)
        public string? Direccion { get; set; }
        public string? DescripcionLugar { get; set; }
        public string? Notas { get; set; }
        public string? NombreHipotetico { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Modelos.DTOs
{
    public class AveDTO
    {
        [Required]
        public string NombreComun { get; set; } = string.Empty;

        [Required]
        public string NombreCientifico { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
    }
}

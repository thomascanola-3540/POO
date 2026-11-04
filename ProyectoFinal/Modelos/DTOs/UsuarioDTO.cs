using System.ComponentModel.DataAnnotations;

namespace Modelos.DTOs
{
    public class UsuarioDTO
    {
        [Required]
        public string Nombres { get; set; } = string.Empty;

        [Required]
        public string Apellidos { get; set; } = string.Empty;

        [EmailAddress]
        [Required]
        public string Correo { get; set; } = string.Empty;

        [MinLength(8)]
        [Required]
        public string Clave { get; set; } = string.Empty;
    }
}

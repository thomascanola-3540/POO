using System.ComponentModel.DataAnnotations;

namespace Modelos.DTOs
{
    public class PerfilDTO
    {
        [Required]
        public string Nombres { get; set; } = string.Empty;

        [Required]
        public string Apellidos { get; set; } = string.Empty;
    }
}

using System.ComponentModel.DataAnnotations;

namespace Modelos.DTOs
{
    public class RolDTO
    {
        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public string Descripcion { get; set; } = string.Empty;
    }
}

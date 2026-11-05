using System.ComponentModel.DataAnnotations;

namespace Modelos.DTOs
{
    public class CiudadDTO
    {
        [Required]
        public string Nombre { get; set; } = string.Empty;
        public int DepartamentoId { get; set; }
    }
}

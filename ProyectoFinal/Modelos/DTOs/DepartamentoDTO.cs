using System.ComponentModel.DataAnnotations;

namespace Modelos.DTOs
{
    public class DepartamentoDTO
    {
        [Required]
        public string Nombre { get; set; } = string.Empty;
    }
}

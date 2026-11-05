using System.ComponentModel.DataAnnotations;

namespace API.JWT
{
    public class LoginModel
    {
        [EmailAddress]
        [Required]
        public string Correo { get; set; } = string.Empty;

        [Required]
        public string Clave { get; set; } = string.Empty;
    }
}

using Modelos;

namespace API.JWT
{
    public interface IJwtServicios
    {
        public string GenerateToken(Usuario usuario);
    }
}

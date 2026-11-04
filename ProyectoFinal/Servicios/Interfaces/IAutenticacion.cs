using Modelos;
using Modelos.DTOs;

namespace Servicios.Interfaces
{
    public interface IAutenticacion
    {
        public Usuario Registrar(UsuarioDTO usuarioDto);

        // Devuelve null si las credenciales no son correctas o la cuenta está inactiva.
        public Usuario? IniciarSesion(string correo, string clave);
    }
}

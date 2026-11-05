using Modelos;
using Modelos.DTOs;

namespace Servicios.Interfaces
{
    public interface IAutenticacion
    {
        public Usuario Registrar(UsuarioDTO usuarioDto);

        public Usuario? IniciarSesion(string correo, string clave);
    }
}

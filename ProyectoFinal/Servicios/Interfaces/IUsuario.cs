using Modelos;
using Modelos.DTOs;

namespace Servicios.Interfaces
{
    public interface IUsuario
    {
        public List<Usuario> GetUsuarios();
        public Usuario? GetUsuarioPorId(int id);
        public void ActualizarPerfil(Usuario usuario, PerfilDTO perfilDto);
        public void CambiarRol(Usuario usuario, Rol rol);
        public void CambiarEstado(Usuario usuario, bool activo);
    }
}

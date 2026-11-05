using Datos;
using Modelos;
using Modelos.DTOs;
using Servicios.Interfaces;

namespace Servicios.Servicios
{
    public class ServicioUsuario : IUsuario
    {
        public List<Usuario> GetUsuarios()
        {
            return ContextoDatos.listaUsuarios;
        }

        public Usuario? GetUsuarioPorId(int id)
        {
            return ContextoDatos.listaUsuarios.FirstOrDefault(item => item.Id == id);
        }

        public void ActualizarPerfil(Usuario usuario, PerfilDTO perfilDto)
        {
            usuario.Nombres = perfilDto.Nombres;
            usuario.Apellidos = perfilDto.Apellidos;
        }

        public void CambiarRol(Usuario usuario, Rol rol)
        {
            if (rol.Nombre == RolesSistema.ColaboradorAnonimo)
            {
                throw new ExcepcionNegocio("El rol de colaborador anónimo no se puede asignar a una cuenta.");
            }
            usuario.Rol = rol;
        }

        public void CambiarEstado(Usuario usuario, bool activo)
        {
            usuario.Activo = activo;
        }
    }
}

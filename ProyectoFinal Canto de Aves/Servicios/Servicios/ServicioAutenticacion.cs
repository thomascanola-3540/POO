using Datos;
using Modelos;
using Modelos.DTOs;
using Servicios.Interfaces;

namespace Servicios.Servicios
{
    public class ServicioAutenticacion : IAutenticacion
    {
        public Usuario Registrar(UsuarioDTO usuarioDto)
        {
            string correo = usuarioDto.Correo.ToLower();
            if (ContextoDatos.listaUsuarios.Any(item => item.Correo == correo))
            {
                throw new ExcepcionNegocio("Ya existe una cuenta con ese correo.");
            }

            Rol rol = ContextoDatos.listaRoles.First(item => item.Nombre == RolesSistema.ColaboradorRegistrado);
            int nuevoId = ContextoDatos.listaUsuarios.Count + 1;
            Usuario usuario = new Usuario(nuevoId, usuarioDto.Nombres, usuarioDto.Apellidos, correo,
                ClaveSegura.Encriptar(usuarioDto.Clave), rol);

            ContextoDatos.listaUsuarios.Add(usuario);
            return usuario;
        }

        public Usuario? IniciarSesion(string correo, string clave)
        {
            Usuario? usuario = ContextoDatos.listaUsuarios.FirstOrDefault(item => item.Correo == correo.ToLower());

            if (usuario == null || !usuario.Activo || !ClaveSegura.Verificar(clave, usuario.Clave))
            {
                return null;
            }
            return usuario;
        }
    }
}

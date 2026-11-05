using Datos;
using Modelos;
using Modelos.DTOs;
using Servicios.Interfaces;

namespace Servicios.Servicios
{
    public class ServicioRol : IRol
    {
        public List<Rol> GetRoles()
        {
            return ContextoDatos.listaRoles;
        }

        public Rol? GetRolPorId(int id)
        {
            return ContextoDatos.listaRoles.FirstOrDefault(item => item.Id == id);
        }

        public Rol CrearRol(RolDTO rolDto)
        {
            ValidarNombreUnico(rolDto.Nombre, 0);

            Rol rol = new Rol();
            rol.Id = ContextoDatos.listaRoles.Count + 1;
            rol.Nombre = rolDto.Nombre;
            rol.Descripcion = rolDto.Descripcion;

            ContextoDatos.listaRoles.Add(rol);
            return rol;
        }

        public void ActualizarRol(Rol rol, RolDTO rolDto)
        {
            ValidarNombreUnico(rolDto.Nombre, rol.Id);

            if (RolesSistema.EsDelSistema(rol.Nombre) && rol.Nombre != rolDto.Nombre)
            {
                throw new ExcepcionNegocio("Los roles del sistema no se pueden renombrar.");
            }

            rol.Nombre = rolDto.Nombre;
            rol.Descripcion = rolDto.Descripcion;
        }

        private void ValidarNombreUnico(string nombre, int idActual)
        {
            if (ContextoDatos.listaRoles.Any(item => item.Nombre.ToLower() == nombre.ToLower() && item.Id != idActual))
            {
                throw new ExcepcionNegocio("Ya existe un rol con ese nombre.");
            }
        }
    }
}

using Modelos;
using Modelos.DTOs;

namespace Servicios.Interfaces
{
    public interface IRol
    {
        public List<Rol> GetRoles();
        public Rol? GetRolPorId(int id);
        public Rol CrearRol(RolDTO rolDto);
        public void ActualizarRol(Rol rol, RolDTO rolDto);
    }
}

using Modelos;
using Modelos.DTOs;

namespace Servicios.Interfaces
{
    public interface ICiudad
    {
        public List<Ciudad> GetCiudades(bool soloActivas);
        public List<Ciudad> GetCiudadesPorDepartamento(int departamentoId);
        public Ciudad? GetCiudadPorId(int id);
        public Ciudad CrearCiudad(CiudadDTO ciudadDto);
        public void ActualizarCiudad(Ciudad ciudad, CiudadDTO ciudadDto);
        public void CambiarEstado(Ciudad ciudad, bool activo);
    }
}

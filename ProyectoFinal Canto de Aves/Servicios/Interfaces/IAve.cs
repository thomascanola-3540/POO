using Modelos;
using Modelos.DTOs;

namespace Servicios.Interfaces
{
    public interface IAve
    {
        public List<Ave> GetAves(bool soloActivas);
        public Ave? GetAvePorId(int id);
        public Ave CrearAve(AveDTO aveDto);
        public void ActualizarAve(Ave ave, AveDTO aveDto);
        public void CambiarEstado(Ave ave, bool activa);
    }
}

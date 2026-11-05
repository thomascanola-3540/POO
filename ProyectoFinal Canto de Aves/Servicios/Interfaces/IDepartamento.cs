using Modelos;
using Modelos.DTOs;

namespace Servicios.Interfaces
{
    public interface IDepartamento
    {
        public List<Departamento> GetDepartamentos(bool soloActivos);
        public Departamento? GetDepartamentoPorId(int id);
        public Departamento CrearDepartamento(DepartamentoDTO departamentoDto);
        public void ActualizarDepartamento(Departamento departamento, DepartamentoDTO departamentoDto);
        public void CambiarEstado(Departamento departamento, bool activo);
    }
}

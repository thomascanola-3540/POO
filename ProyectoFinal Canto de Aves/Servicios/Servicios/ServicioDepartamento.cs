using Datos;
using Modelos;
using Modelos.DTOs;
using Servicios.Interfaces;

namespace Servicios.Servicios
{
    public class ServicioDepartamento : IDepartamento
    {
        public List<Departamento> GetDepartamentos(bool soloActivos)
        {
            return ContextoDatos.listaDepartamentos.Where(item => !soloActivos || item.Activo).ToList();
        }

        public Departamento? GetDepartamentoPorId(int id)
        {
            return ContextoDatos.listaDepartamentos.FirstOrDefault(item => item.Id == id);
        }

        public Departamento CrearDepartamento(DepartamentoDTO departamentoDto)
        {
            ValidarNombreUnico(departamentoDto.Nombre, 0);

            Departamento departamento = new Departamento();
            departamento.Id = ContextoDatos.listaDepartamentos.Count + 1;
            departamento.Nombre = departamentoDto.Nombre;

            ContextoDatos.listaDepartamentos.Add(departamento);
            return departamento;
        }

        public void ActualizarDepartamento(Departamento departamento, DepartamentoDTO departamentoDto)
        {
            ValidarNombreUnico(departamentoDto.Nombre, departamento.Id);
            departamento.Nombre = departamentoDto.Nombre;
        }

        public void CambiarEstado(Departamento departamento, bool activo)
        {
            departamento.Activo = activo;
        }

        private void ValidarNombreUnico(string nombre, int idActual)
        {
            if (ContextoDatos.listaDepartamentos.Any(item => item.Nombre.ToLower() == nombre.ToLower() && item.Id != idActual))
            {
                throw new ExcepcionNegocio("Ya existe un departamento con ese nombre.");
            }
        }
    }
}

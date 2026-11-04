using Datos;
using Modelos;
using Modelos.DTOs;
using Servicios.Interfaces;

namespace Servicios.Servicios
{
    public class ServicioCiudad : ICiudad
    {
        private readonly IDepartamento _departamento;

        public ServicioCiudad(IDepartamento departamento)
        {
            _departamento = departamento;
        }

        public List<Ciudad> GetCiudades(bool soloActivas)
        {
            return ContextoDatos.listaCiudades.Where(item => !soloActivas || EstaDisponible(item)).ToList();
        }

        public List<Ciudad> GetCiudadesPorDepartamento(int departamentoId)
        {
            return GetCiudades(true).Where(item => item.Departamento.Id == departamentoId).ToList();
        }

        public Ciudad? GetCiudadPorId(int id)
        {
            return ContextoDatos.listaCiudades.FirstOrDefault(item => item.Id == id);
        }

        public Ciudad CrearCiudad(CiudadDTO ciudadDto)
        {
            Departamento departamento = ObtenerDepartamento(ciudadDto.DepartamentoId);
            ValidarNombreUnico(ciudadDto.Nombre, departamento.Id, 0);

            int nuevoId = ContextoDatos.listaCiudades.Count + 1;
            Ciudad ciudad = new Ciudad(nuevoId, ciudadDto.Nombre, departamento);

            ContextoDatos.listaCiudades.Add(ciudad);
            return ciudad;
        }

        public void ActualizarCiudad(Ciudad ciudad, CiudadDTO ciudadDto)
        {
            Departamento departamento = ObtenerDepartamento(ciudadDto.DepartamentoId);
            ValidarNombreUnico(ciudadDto.Nombre, departamento.Id, ciudad.Id);

            ciudad.Nombre = ciudadDto.Nombre;
            ciudad.Departamento = departamento;
        }

        public void CambiarEstado(Ciudad ciudad, bool activo)
        {
            ciudad.Activo = activo;
        }

        private bool EstaDisponible(Ciudad ciudad)
        {
            return ciudad.Activo && ciudad.Departamento.Activo;
        }

        private Departamento ObtenerDepartamento(int departamentoId)
        {
            Departamento? departamento = _departamento.GetDepartamentoPorId(departamentoId);
            if (departamento == null || !departamento.Activo)
            {
                throw new ExcepcionNegocio("El departamento no existe o está inactivo.");
            }
            return departamento;
        }

        private void ValidarNombreUnico(string nombre, int departamentoId, int idActual)
        {
            if (ContextoDatos.listaCiudades.Any(item => item.Nombre.ToLower() == nombre.ToLower()
                && item.Departamento.Id == departamentoId && item.Id != idActual))
            {
                throw new ExcepcionNegocio("El departamento ya tiene una ciudad con ese nombre.");
            }
        }
    }
}

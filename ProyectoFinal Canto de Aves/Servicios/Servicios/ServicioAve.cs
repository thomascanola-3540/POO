using Datos;
using Modelos;
using Modelos.DTOs;
using Servicios.Interfaces;

namespace Servicios.Servicios
{
    public class ServicioAve : IAve
    {
        public List<Ave> GetAves(bool soloActivas)
        {
            return ContextoDatos.listaAves.Where(item => !soloActivas || item.Activa).ToList();
        }

        public Ave? GetAvePorId(int id)
        {
            return ContextoDatos.listaAves.FirstOrDefault(item => item.Id == id);
        }

        public Ave CrearAve(AveDTO aveDto)
        {
            ValidarNombreUnico(aveDto.NombreCientifico, 0);

            Ave ave = new Ave();
            ave.Id = ContextoDatos.listaAves.Count + 1;
            ave.NombreComun = aveDto.NombreComun;
            ave.NombreCientifico = aveDto.NombreCientifico;
            ave.Descripcion = aveDto.Descripcion;

            ContextoDatos.listaAves.Add(ave);
            return ave;
        }

        public void ActualizarAve(Ave ave, AveDTO aveDto)
        {
            ValidarNombreUnico(aveDto.NombreCientifico, ave.Id);

            ave.NombreComun = aveDto.NombreComun;
            ave.NombreCientifico = aveDto.NombreCientifico;
            ave.Descripcion = aveDto.Descripcion;
        }

        public void CambiarEstado(Ave ave, bool activa)
        {
            ave.Activa = activa;
        }

        private void ValidarNombreUnico(string nombreCientifico, int idActual)
        {
            if (ContextoDatos.listaAves.Any(item => item.NombreCientifico.ToLower() == nombreCientifico.ToLower() && item.Id != idActual))
            {
                throw new ExcepcionNegocio("Ya existe un ave con ese nombre científico.");
            }
        }
    }
}

using Modelos;
using Modelos.DTOs;

namespace Servicios.Interfaces
{
    public interface IArchivo
    {
        public Archivo? GetArchivoPorId(int id);
        public ArchivoAudio CrearAudio(ArchivoDTO archivoDto);
        public List<ArchivoImagen> CrearImagenes(List<ArchivoDTO> archivosDto);
        public void GuardarArchivo(Archivo archivo);
    }
}

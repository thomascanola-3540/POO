using Datos;
using Modelos;
using Modelos.DTOs;
using Servicios.Interfaces;

namespace Servicios.Servicios
{
    public class ServicioArchivo : IArchivo
    {
        public Archivo? GetArchivoPorId(int id)
        {
            return ContextoDatos.listaArchivos.FirstOrDefault(item => item.Id == id);
        }

        public ArchivoAudio CrearAudio(ArchivoDTO archivoDto)
        {
            ArchivoAudio audio = new ArchivoAudio(archivoDto.NombreOriginal, archivoDto.Contenido);
            audio.Validar();
            return audio;
        }

        public List<ArchivoImagen> CrearImagenes(List<ArchivoDTO> archivosDto)
        {
            if (archivosDto.Count > ConfiguracionArchivos.ObtenerInstancia.MaximoImagenesPorEvento)
            {
                throw new ExcepcionNegocio($"Solo se permiten {ConfiguracionArchivos.ObtenerInstancia.MaximoImagenesPorEvento} imágenes por evento.");
            }

            List<ArchivoImagen> imagenes = new List<ArchivoImagen>();
            foreach (ArchivoDTO archivoDto in archivosDto)
            {
                ArchivoImagen imagen = new ArchivoImagen(archivoDto.NombreOriginal, archivoDto.Contenido);
                imagen.Validar();
                imagenes.Add(imagen);
            }
            return imagenes;
        }

        // Recibe un Archivo: sirve igual para audios e imágenes (sustitución de Liskov).
        public void GuardarArchivo(Archivo archivo)
        {
            archivo.Id = ContextoDatos.listaArchivos.Count + 1;
            archivo.Ruta = $"/api/Archivo/Reproducir?id={archivo.Id}";
            ContextoDatos.listaArchivos.Add(archivo);
        }
    }
}

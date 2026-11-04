namespace Modelos.DTOs
{
    // Archivo recibido, ya sin depender de IFormFile: así Servicios no necesita conocer ASP.NET.
    public class ArchivoDTO
    {
        public string NombreOriginal { get; set; } = string.Empty;
        public byte[] Contenido { get; set; } = new byte[0];
    }
}

namespace Modelos.DTOs
{
    public class ArchivoDTO
    {
        public string NombreOriginal { get; set; } = string.Empty;
        public byte[] Contenido { get; set; } = new byte[0];
    }
}

namespace Modelos.DTOs
{
    // Lo que la API muestra de un archivo: sus datos y la ruta para reproducirlo, no los bytes.
    public class ArchivoRespuestaDTO
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public string NombreOriginal { get; set; }
        public string Ruta { get; set; }
        public long TamanoBytes { get; set; }
        public DateTime FechaCarga { get; set; }

        public ArchivoRespuestaDTO(Archivo archivo)
        {
            Id = archivo.Id;
            Tipo = archivo.Tipo;
            NombreOriginal = archivo.NombreOriginal;
            Ruta = archivo.Ruta;
            TamanoBytes = archivo.TamanoBytes;
            FechaCarga = archivo.FechaCarga;
        }
    }
}

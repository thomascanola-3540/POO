namespace Modelos
{
    public class Ave
    {
        public int Id { get; set; }
        public string NombreComun { get; set; } = string.Empty;
        public string NombreCientifico { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public bool Activa { get; set; } = true;
    }
}

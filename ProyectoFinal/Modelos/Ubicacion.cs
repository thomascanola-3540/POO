namespace Modelos
{
    public class Ubicacion
    {
        public int Id { get; set; }
        public double Longitud { get; set; }
        public double Latitud { get; set; }
        public string? Direccion { get; set; }
        public string? Descripcion { get; set; }
        public Ciudad Ciudad { get; set; } // Cada ubicación pertenece a una única ciudad (RN08)

        public Ubicacion(int id, double longitud, double latitud, string? direccion, string? descripcion, Ciudad ciudad)
        {
            if (longitud < -180 || longitud > 180)
            {
                throw new ExcepcionNegocio("La longitud debe estar entre -180 y 180.");
            }
            if (latitud < -90 || latitud > 90)
            {
                throw new ExcepcionNegocio("La latitud debe estar entre -90 y 90.");
            }

            Id = id;
            Longitud = longitud;
            Latitud = latitud;
            Direccion = direccion;
            Descripcion = descripcion;
            Ciudad = ciudad;
        }
    }
}

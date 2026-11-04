namespace Modelos
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public Rol Rol { get; set; } // Cada usuario tiene un único rol (RN04)

        public Usuario(int id, string nombres, string apellidos, string correo, string clave, Rol rol)
        {
            Id = id;
            Nombres = nombres;
            Apellidos = apellidos;
            Correo = correo;
            Clave = clave;
            Rol = rol;
            Activo = true;
            FechaRegistro = DateTime.Now;
        }

        public string NombreCompleto()
        {
            return $"{Nombres} {Apellidos}";
        }
    }
}

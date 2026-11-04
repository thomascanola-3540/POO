namespace Modelos.DTOs
{
    // Lo que la API muestra de un usuario. Nunca incluye la clave.
    public class UsuarioRespuestaDTO
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public string Rol { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }

        public UsuarioRespuestaDTO(Usuario usuario)
        {
            Id = usuario.Id;
            Nombres = usuario.Nombres;
            Apellidos = usuario.Apellidos;
            Correo = usuario.Correo;
            Rol = usuario.Rol.Nombre;
            Activo = usuario.Activo;
            FechaRegistro = usuario.FechaRegistro;
        }
    }
}

namespace Modelos
{
    public class Evento
    {
        public int Id { get; set; }
        public DateTime FechaHora { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string? Notas { get; set; }
        public string? NombreHipotetico { get; set; }
        public string EstadoValidacion { get; private set; } = "Pendiente";
        public bool DesvinculadoDelPerfil { get; private set; }
        public Ubicacion Ubicacion { get; set; }
        public Usuario? Usuario { get; set; }
        public Ave? AveIdentificada { get; private set; }
        public List<Archivo> Archivos { get; } = new List<Archivo>();

        public Evento(int id, DateTime fechaHora, Ubicacion ubicacion, Usuario? usuario, string? notas, string? nombreHipotetico)
        {
            if (fechaHora > DateTime.Now)
            {
                throw new ExcepcionNegocio("La fecha y hora del evento no puede estar en el futuro.");
            }

            Id = id;
            FechaHora = fechaHora;
            FechaRegistro = DateTime.Now;
            Ubicacion = ubicacion;
            Usuario = usuario;
            Notas = notas;
            NombreHipotetico = nombreHipotetico;
        }

        public void AdjuntarArchivo(Archivo archivo)
        {
            Archivos.Add(archivo);
        }

        public bool PerteneceA(int idUsuario)
        {
            return Usuario != null && Usuario.Id == idUsuario;
        }

        public void Validar(Ave ave)
        {
            AveIdentificada = ave;
            EstadoValidacion = "Validado";
        }

        public void Rechazar()
        {
            AveIdentificada = null;
            EstadoValidacion = "Rechazado";
        }

        public void Desvincular()
        {
            DesvinculadoDelPerfil = true;
        }
    }
}

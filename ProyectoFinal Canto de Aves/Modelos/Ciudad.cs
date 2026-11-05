namespace Modelos
{
    public class Ciudad
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        public Departamento Departamento { get; set; } 

        public Ciudad(int id, string nombre, Departamento departamento)
        {
            Id = id;
            Nombre = nombre;
            Departamento = departamento;
            Activo = true;
        }
    }
}

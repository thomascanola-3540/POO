namespace Modelos
{
    // Se lanza cuando no se cumple una regla de negocio. El controlador la atrapa y responde 400.
    public class ExcepcionNegocio : Exception
    {
        public ExcepcionNegocio(string mensaje) : base(mensaje)
        {
        }
    }
}

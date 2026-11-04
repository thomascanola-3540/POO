namespace Servicios.Comandos
{
    // Patrón Command: cada acción sobre un evento se convierte en un objeto independiente.
    public interface IComandoEvento
    {
        void Ejecutar();
    }
}

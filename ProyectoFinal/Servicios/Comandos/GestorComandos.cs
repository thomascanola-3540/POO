namespace Servicios.Comandos
{
    // Es el invocador (como el ControlRemoto del ejemplo de clase): ejecuta un comando sin saber qué hace.
    public class GestorComandos
    {
        private IComandoEvento? _comando;

        public void ConfigurarComando(IComandoEvento comando)
        {
            _comando = comando;
        }

        public void EjecutarComando()
        {
            _comando?.Ejecutar();
        }
    }
}

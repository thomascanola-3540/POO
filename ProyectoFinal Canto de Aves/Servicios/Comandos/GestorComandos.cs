namespace Servicios.Comandos
{
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

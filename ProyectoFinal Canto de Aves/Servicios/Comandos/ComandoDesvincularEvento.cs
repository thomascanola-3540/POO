using Modelos;

namespace Servicios.Comandos
{
    public class ComandoDesvincularEvento : IComandoEvento
    {
        private Evento _evento;

        public ComandoDesvincularEvento(Evento evento) => _evento = evento;

        public void Ejecutar() => _evento.Desvincular();
    }
}

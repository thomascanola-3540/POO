using Modelos;

namespace Servicios.Comandos
{
    public class ComandoRechazarEvento : IComandoEvento
    {
        private Evento _evento;

        public ComandoRechazarEvento(Evento evento) => _evento = evento;

        public void Ejecutar() => _evento.Rechazar();
    }
}

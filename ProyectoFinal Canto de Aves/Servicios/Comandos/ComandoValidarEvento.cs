using Modelos;

namespace Servicios.Comandos
{
    public class ComandoValidarEvento : IComandoEvento
    {
        private Evento _evento;
        private Ave _ave;

        public ComandoValidarEvento(Evento evento, Ave ave)
        {
            _evento = evento;
            _ave = ave;
        }

        public void Ejecutar() => _evento.Validar(_ave);
    }
}

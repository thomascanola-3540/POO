using System;
using System.Collections.Generic;
using System.Text;

namespace Patron_command
{
    public class ComandoEncender : IComando
    {
        private Luz _luz;
        public ComandoEncender(Luz luz) => _luz = luz;

        public void Ejecutar() => _luz.Encender();
    }
}

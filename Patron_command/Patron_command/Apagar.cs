using System;
using System.Collections.Generic;
using System.Text;

namespace Patron_command
{
    public class ComandoApagar : IComando
    {
        private Luz _luz;
        public ComandoApagar(Luz luz) => _luz = luz;

        public void Ejecutar() => _luz.Apagar();
    }
}

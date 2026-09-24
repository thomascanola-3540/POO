using System;
using System.Collections.Generic;
using System.Text;

namespace Patron_command
{
    public class ControlRemoto
    {
        private IComando _comando;

        public void ConfigurarBoton(IComando comando)
        {
            _comando = comando;
        }

        public void PresionarBoton()
        {
            _comando.Ejecutar();
        }
    }
}

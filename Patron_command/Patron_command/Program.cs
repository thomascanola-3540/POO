using System;
using System.Collections.Generic;
using System.Text;

namespace Patron_command
{
    public class Program
    {
        public static void Main()
        {
            Luz lampara = new Luz();

            IComando encender = new ComandoEncender(lampara);
            IComando apagar = new ComandoApagar(lampara);

            ControlRemoto control = new ControlRemoto();

            control.ConfigurarBoton(encender);
            control.PresionarBoton();

            control.ConfigurarBoton(apagar);
            control.PresionarBoton();
        }
    }
}
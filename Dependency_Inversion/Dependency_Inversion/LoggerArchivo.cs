using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dependency_Inversion
{
    public class LoggerArchivo : ILogger
    {
        public void Registrar(string mensaje)
        {
            Console.WriteLine("[Archivo simulado] " + mensaje);
        }
    }

}

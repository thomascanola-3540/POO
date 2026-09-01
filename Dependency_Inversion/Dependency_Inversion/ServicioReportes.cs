using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dependency_Inversion
{
    public class ServicioReportes
    {
        private ILogger logger;

        public ServicioReportes(ILogger loggerRecibido)
        {
            logger = loggerRecibido;
        }

        public void GenerarReporte()
        {
            logger.Registrar("Generando reporte...");
        }
    }
}

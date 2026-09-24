using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liskob_Substitution
{
    public class Ave
    {
        public string Nombre { get; set; }

        public virtual string Moverse() => $"{Nombre} se mueve caminando";
        public virtual string Saludar() { return "Hola"; }
    }
}

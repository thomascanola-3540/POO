using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_Segregation
{
    public class Desarrollador : IProgramador
    {
        public void Programar()
        {
            Console.WriteLine("Escribiendo código...");
        }
    }
}

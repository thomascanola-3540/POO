using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_Segregation
{
    public class DesarrolladorFullStack : IProgramador, IDisenador
    {
        public void Programar()
        {
            Console.WriteLine("Escribiendo código...");
        }

        public void DisenarUI()
        {
            Console.WriteLine("Diseñando la interfaz...");
        }
    }
}

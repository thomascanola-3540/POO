using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Open_Close
{
    public class Cuadrado : Figura
    {
        public double Lado { get; set; }

        public override double Area() => this.Lado * this.Lado;
    }
}

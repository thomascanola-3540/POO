using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Open_Close
{
    public class Trapecio : Figura
    {
        public double BaseMayor { get; set; }
        public double BaseMenor { get; set; }
        public double Altura { get; set; }

        public override double Area() => ((this.BaseMayor + this.BaseMenor) * this.Altura) / 2;
    }
}

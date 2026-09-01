using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Open_Close
{
    public class Circulo : Figura
    {
        public double Radius { get; set; }

        public override double Area() => (this.Radius * this.Radius * Math.PI);
    }
}

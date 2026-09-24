using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Open_Close
{
    public class AreaCalculator
    {

        public double TotalArea(List<Figura> shapes) => shapes.Sum(item => item.Area());

    }
}

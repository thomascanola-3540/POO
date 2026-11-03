using System;
using System.Collections.Generic;
using System.Text;
using Modelos;

namespace Servicios.Interfaces
{
    public interface IEmpleado
    {
        public List<Empleado> GetEmpleados();

        public void CrearEmpleado(Empleado empleado);
    }
}

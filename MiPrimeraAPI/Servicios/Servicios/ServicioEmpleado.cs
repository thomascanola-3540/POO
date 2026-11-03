using Datos;
using Modelos;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace Servicios.Servicios
{
    public class ServicioEmpleado : IEmpleado
    {
        public List<Empleado> GetEmpleados()
        {
            return ContextoDatos.listaEmpleados;
        }

        public void CrearEmpleado(Empleado empleado)
        {
            ContextoDatos.listaEmpleados.Add(empleado);
        }
    }
}

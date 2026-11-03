using System;
using System.Collections.Generic;
using System.Text;
using Modelos;

namespace Datos
{
    public class ContextoDatos
    {
        public static List<Empleado> listaEmpleados = new List<Empleado>()
        {
            new Empleado()
            {
                Id = 1,
                Nombre = "Thomas",
                Apellido = "Cañola"
            },
            new Empleado
            {
                Id = 2,
                Nombre = "Isabela",
                Apellido = "Osorio"
            },
            new Empleado
            {
                Id = 3,
                Nombre = "Lina",
                Apellido = "Castrillon"
            },
            new Empleado
            {
                Id = 4,
                Nombre = "Jaber",
                Apellido = "Cañola"
            }
        };
    }
}

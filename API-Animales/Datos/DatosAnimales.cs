using System;
using System.Collections.Generic;
using System.Text;
using Modelos;

namespace Datos
{
    public class DatosAnimales
    {
        public static List<Animal> listaAnimales = new List<Animal>()
        {
            new Animal()
            {
                Nombre = "Ginger",
                Tipo = "Gata",
                Edad = 1,
                Id = 1

            }
        };
    }
}

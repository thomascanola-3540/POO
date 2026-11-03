using System;
using System.Collections.Generic;
using System.Text;
using Datos;
using Modelos;
using Servicios.Interfaces;

namespace Servicios.ServicioAnimal
{
    public class ServicioAnimal : IAnimal 
    {
        public List<Animal> GetAnimales()
        {
            return DatosAnimales.listaAnimales;
        }

        public void CrearAnimal(Animal animal)
        {
            DatosAnimales.listaAnimales.Add(animal);
        }
    }
}

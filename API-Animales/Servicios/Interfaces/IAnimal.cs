using System;
using System.Collections.Generic;
using System.Text;
using Modelos;

namespace Servicios.Interfaces
{
    public interface IAnimal
    {
        public List<Animal> GetAnimales();
        public void CrearAnimal(Animal animal);
    }
}

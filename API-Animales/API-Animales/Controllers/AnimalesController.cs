using Microsoft.AspNetCore.Mvc;
using Modelos.DTOs;
using Servicios.Interfaces;
using Modelos;

namespace API_Animales.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalesController : ControllerBase
    {
        private readonly IAnimal _animal;

        public AnimalesController(IAnimal animal)
        {
            _animal = animal;
        }

        [HttpGet("GetAnimalesPorNombre")]
        public IActionResult GetAnimalesNombre(string nombre)
        {
            var animales = _animal.GetAnimales().Where
                (item => item.Nombre.ToLower().Contains(nombre.ToLower()));
            return Ok(animales);
        }

        [HttpGet("GetAnimalesPorID")]
        public IActionResult GetAnimalId(int id)
        {
            var animal = _animal.GetAnimales().Where
                (item => item.Id == id).FirstOrDefault();
            return Ok(animal);
        }
        
        [HttpGet("GetAnimales")]
        public IActionResult GetAnimales()
        {
            return Ok(_animal.GetAnimales());
        }

        [HttpPost("AgregarAnimal")]
        public IActionResult AddAnimal(AnimalDTO animalDto)
        {
            int nuevoId = _animal.GetAnimales().Count + 1;
            var AnimalBD = new Animal();
            AnimalBD.Id = nuevoId;
            AnimalBD.Nombre = animalDto.Nombre;
            AnimalBD.Tipo = animalDto.Tipo; 
            AnimalBD.Edad = animalDto.Edad;

            _animal.CrearAnimal(AnimalBD);
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}

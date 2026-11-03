using Microsoft.AspNetCore.Mvc;
using Modelos.DTOs;
using Modelos;
using Servicios;
using Servicios.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace MiPrimeraAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize] Para todos los endpoints
    public class EmpleadoController : ControllerBase
    {
        private readonly IEmpleado _empleado;

        public EmpleadoController(IEmpleado empleado) 
        {
            _empleado = empleado;
        }

        [Authorize]
        [HttpGet("GetEmpleados")]
        public IActionResult GetEmpleados()
        {
            return Ok(_empleado.GetEmpleados());
        }

        [HttpGet("GetEmpleadoPorId")]
        public IActionResult GetEmpleadoId(int id)
        {
            var empleado = _empleado.GetEmpleados().Where
                (item => item.Id == id).FirstOrDefault();
            return Ok(empleado);
        }

        [HttpPost("AgregarEmpleado")]
        public IActionResult AddEmpleado(EmpleadoDTO empleadoDto)
        {
            int nuevoId = _empleado.GetEmpleados().Count + 1;
            var empleadoBD = new Empleado();
            empleadoBD.Id = nuevoId;
            empleadoBD.Nombre = empleadoDto.Nombre;
            empleadoBD.Apellido = empleadoDto.Apellido;

            _empleado.CrearEmpleado(empleadoBD);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpGet("GetEmpleadoPorNombre")]
        public IActionResult GetEmpleadoPorNombre(string nombre)
        {
            var empleados = _empleado.GetEmpleados().Where
                (item => item.Nombre.ToLower().Contains(nombre.ToLower())).ToList();
            return Ok(empleados);
        }
    }
}

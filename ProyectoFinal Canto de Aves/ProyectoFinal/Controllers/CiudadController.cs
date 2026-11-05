using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos;
using Modelos.DTOs;
using Servicios.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CiudadController : ControllerBase
    {
        private readonly ICiudad _ciudad;

        public CiudadController(ICiudad ciudad)
        {
            _ciudad = ciudad;
        }

        [HttpGet("GetCiudades")]
        public IActionResult GetCiudades()
        {
            return Ok(_ciudad.GetCiudades(true));
        }

        [HttpGet("GetCiudadesPorDepartamento")]
        public IActionResult GetCiudadesPorDepartamento(int departamentoId)
        {
            return Ok(_ciudad.GetCiudadesPorDepartamento(departamentoId));
        }

        [Authorize(Roles = RolesSistema.Administrador)]
        [HttpGet("GetTodasLasCiudades")]
        public IActionResult GetTodasLasCiudades()
        {
            return Ok(_ciudad.GetCiudades(false));
        }

        [Authorize(Roles = RolesSistema.Administrador)]
        [HttpPost("AgregarCiudad")]
        public IActionResult AddCiudad(CiudadDTO ciudadDto)
        {
            try
            {
                return StatusCode(StatusCodes.Status201Created, _ciudad.CrearCiudad(ciudadDto));
            }
            catch (ExcepcionNegocio ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = RolesSistema.Administrador)]
        [HttpPut("ActualizarCiudad")]
        public IActionResult UpdateCiudad(int id, CiudadDTO ciudadDto)
        {
            Ciudad? ciudad = _ciudad.GetCiudadPorId(id);
            if (ciudad == null)
            {
                return NotFound();
            }

            try
            {
                _ciudad.ActualizarCiudad(ciudad, ciudadDto);
                return Ok(ciudad);
            }
            catch (ExcepcionNegocio ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = RolesSistema.Administrador)]
        [HttpPut("CambiarEstado")]
        public IActionResult CambiarEstado(int id, bool activo)
        {
            Ciudad? ciudad = _ciudad.GetCiudadPorId(id);
            if (ciudad == null)
            {
                return NotFound();
            }

            _ciudad.CambiarEstado(ciudad, activo);
            return Ok(ciudad);
        }
    }
}

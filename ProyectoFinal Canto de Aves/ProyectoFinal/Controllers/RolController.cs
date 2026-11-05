using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos;
using Modelos.DTOs;
using Servicios.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = RolesSistema.Administrador)]
    public class RolController : ControllerBase
    {
        private readonly IRol _rol;

        public RolController(IRol rol)
        {
            _rol = rol;
        }

        [HttpGet("GetRoles")]
        public IActionResult GetRoles()
        {
            return Ok(_rol.GetRoles());
        }

        [HttpPost("AgregarRol")]
        public IActionResult AddRol(RolDTO rolDto)
        {
            try
            {
                return StatusCode(StatusCodes.Status201Created, _rol.CrearRol(rolDto));
            }
            catch (ExcepcionNegocio ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("ActualizarRol")]
        public IActionResult UpdateRol(int id, RolDTO rolDto)
        {
            Rol? rol = _rol.GetRolPorId(id);
            if (rol == null)
            {
                return NotFound();
            }

            try
            {
                _rol.ActualizarRol(rol, rolDto);
                return Ok(rol);
            }
            catch (ExcepcionNegocio ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

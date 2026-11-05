using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos;
using Modelos.DTOs;
using Servicios.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AveController : ControllerBase
    {
        private readonly IAve _ave;

        public AveController(IAve ave)
        {
            _ave = ave;
        }

        [HttpGet("GetAves")]
        public IActionResult GetAves()
        {
            return Ok(_ave.GetAves(true));
        }

        [Authorize(Roles = RolesSistema.Administrador)]
        [HttpGet("GetTodasLasAves")]
        public IActionResult GetTodasLasAves()
        {
            return Ok(_ave.GetAves(false));
        }

        [Authorize(Roles = RolesSistema.Administrador)]
        [HttpPost("AgregarAve")]
        public IActionResult AddAve(AveDTO aveDto)
        {
            try
            {
                return StatusCode(StatusCodes.Status201Created, _ave.CrearAve(aveDto));
            }
            catch (ExcepcionNegocio ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = RolesSistema.Administrador)]
        [HttpPut("ActualizarAve")]
        public IActionResult UpdateAve(int id, AveDTO aveDto)
        {
            Ave? ave = _ave.GetAvePorId(id);
            if (ave == null)
            {
                return NotFound();
            }

            try
            {
                _ave.ActualizarAve(ave, aveDto);
                return Ok(ave);
            }
            catch (ExcepcionNegocio ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = RolesSistema.Administrador)]
        [HttpPut("CambiarEstado")]
        public IActionResult CambiarEstado(int id, bool activa)
        {
            Ave? ave = _ave.GetAvePorId(id);
            if (ave == null)
            {
                return NotFound();
            }

            _ave.CambiarEstado(ave, activa);
            return Ok(ave);
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos;
using Modelos.DTOs;
using Servicios.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {
        private readonly IDepartamento _departamento;

        public DepartamentoController(IDepartamento departamento)
        {
            _departamento = departamento;
        }

        [HttpGet("GetDepartamentos")]
        public IActionResult GetDepartamentos()
        {
            return Ok(_departamento.GetDepartamentos(true));
        }

        [Authorize(Roles = RolesSistema.Administrador)]
        [HttpGet("GetTodosLosDepartamentos")]
        public IActionResult GetTodosLosDepartamentos()
        {
            return Ok(_departamento.GetDepartamentos(false));
        }

        [Authorize(Roles = RolesSistema.Administrador)]
        [HttpPost("AgregarDepartamento")]
        public IActionResult AddDepartamento(DepartamentoDTO departamentoDto)
        {
            try
            {
                return StatusCode(StatusCodes.Status201Created, _departamento.CrearDepartamento(departamentoDto));
            }
            catch (ExcepcionNegocio ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = RolesSistema.Administrador)]
        [HttpPut("ActualizarDepartamento")]
        public IActionResult UpdateDepartamento(int id, DepartamentoDTO departamentoDto)
        {
            Departamento? departamento = _departamento.GetDepartamentoPorId(id);
            if (departamento == null)
            {
                return NotFound();
            }

            try
            {
                _departamento.ActualizarDepartamento(departamento, departamentoDto);
                return Ok(departamento);
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
            Departamento? departamento = _departamento.GetDepartamentoPorId(id);
            if (departamento == null)
            {
                return NotFound();
            }

            _departamento.CambiarEstado(departamento, activo);
            return Ok(departamento);
        }
    }
}

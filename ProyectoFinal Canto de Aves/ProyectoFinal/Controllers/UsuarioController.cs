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
    public class UsuarioController : BaseController
    {
        private readonly IUsuario _usuario;
        private readonly IRol _rol;

        public UsuarioController(IUsuario usuario, IRol rol)
        {
            _usuario = usuario;
            _rol = rol;
        }

        [HttpGet("GetUsuarios")]
        public IActionResult GetUsuarios()
        {
            return Ok(_usuario.GetUsuarios().Select(item => new UsuarioRespuestaDTO(item)));
        }

        [HttpGet("GetUsuarioPorId")]
        public IActionResult GetUsuarioId(int id)
        {
            Usuario? usuario = _usuario.GetUsuarioPorId(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(new UsuarioRespuestaDTO(usuario));
        }

        [HttpPut("CambiarRol")]
        public IActionResult CambiarRol(int id, int rolId)
        {
            Usuario? usuario = _usuario.GetUsuarioPorId(id);
            Rol? rol = _rol.GetRolPorId(rolId);
            if (usuario == null || rol == null)
            {
                return NotFound();
            }
            if (id == ObtenerIdUsuario())
            {
                return BadRequest("No puede cambiar su propio rol.");
            }

            try
            {
                _usuario.CambiarRol(usuario, rol);
                return Ok(new UsuarioRespuestaDTO(usuario));
            }
            catch (ExcepcionNegocio ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("CambiarEstado")]
        public IActionResult CambiarEstado(int id, bool activo)
        {
            Usuario? usuario = _usuario.GetUsuarioPorId(id);
            if (usuario == null)
            {
                return NotFound();
            }
            if (id == ObtenerIdUsuario())
            {
                return BadRequest("No puede cambiar el estado de su propia cuenta.");
            }

            _usuario.CambiarEstado(usuario, activo);
            return Ok(new UsuarioRespuestaDTO(usuario));
        }
    }
}

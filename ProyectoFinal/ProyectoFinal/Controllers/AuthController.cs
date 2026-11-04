using API.JWT;
using Microsoft.AspNetCore.Mvc;
using Modelos;
using Modelos.DTOs;
using Servicios.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAutenticacion _autenticacion;
        private readonly IJwtServicios _jwtService;

        public AuthController(IAutenticacion autenticacion, IJwtServicios jwtService)
        {
            _autenticacion = autenticacion;
            _jwtService = jwtService;
        }

        // RF01: el colaborador anónimo crea su cuenta.
        [HttpPost("Registro")]
        public IActionResult Registro(UsuarioDTO usuarioDto)
        {
            try
            {
                Usuario usuario = _autenticacion.Registrar(usuarioDto);
                return StatusCode(StatusCodes.Status201Created, CrearRespuesta(usuario));
            }
            catch (ExcepcionNegocio ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // RF02: inicio de sesión con correo y clave.
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            Usuario? usuario = _autenticacion.IniciarSesion(model.Correo, model.Clave);
            if (usuario == null)
            {
                return Unauthorized();
            }
            return Ok(CrearRespuesta(usuario));
        }

        private object CrearRespuesta(Usuario usuario)
        {
            return new { Token = _jwtService.GenerateToken(usuario), Usuario = new UsuarioRespuestaDTO(usuario) };
        }
    }
}

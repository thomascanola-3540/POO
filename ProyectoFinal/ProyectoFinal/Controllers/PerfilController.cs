using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos;
using Modelos.DTOs;
using Servicios.Interfaces;

namespace API.Controllers
{
    // RF03 y RF07: lo que el colaborador registrado hace desde su perfil.
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = RolesSistema.ColaboradorRegistrado)]
    public class PerfilController : BaseController
    {
        private readonly IUsuario _usuario;
        private readonly IEvento _evento;

        public PerfilController(IUsuario usuario, IEvento evento)
        {
            _usuario = usuario;
            _evento = evento;
        }

        [HttpGet("GetPerfil")]
        public IActionResult GetPerfil()
        {
            Usuario? usuario = _usuario.GetUsuarioPorId(ObtenerIdUsuario()!.Value);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(new UsuarioRespuestaDTO(usuario));
        }

        [HttpPut("ActualizarPerfil")]
        public IActionResult ActualizarPerfil(PerfilDTO perfilDto)
        {
            Usuario? usuario = _usuario.GetUsuarioPorId(ObtenerIdUsuario()!.Value);
            if (usuario == null)
            {
                return NotFound();
            }

            _usuario.ActualizarPerfil(usuario, perfilDto);
            return Ok(new UsuarioRespuestaDTO(usuario));
        }

        [HttpGet("GetMisEventos")]
        public IActionResult GetMisEventos()
        {
            var eventos = _evento.GetEventosDeUsuario(ObtenerIdUsuario()!.Value);
            return Ok(eventos.Select(item => new EventoRespuestaDTO(item)));
        }

        // RN05: el colaborador solo puede desvincular de su perfil sus propios eventos.
        [HttpPut("DesvincularEvento")]
        public IActionResult DesvincularEvento(int id)
        {
            Evento? evento = _evento.GetEventoPorId(id);
            if (evento == null)
            {
                return NotFound();
            }
            if (!evento.PerteneceA(ObtenerIdUsuario()!.Value))
            {
                return Forbid();
            }

            _evento.DesvincularEvento(evento);
            return NoContent();
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos;
using Modelos.DTOs;
using Servicios.Interfaces;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : BaseController
    {
        private readonly IEvento _evento;
        private readonly IDataset _dataset;

        public EventoController(IEvento evento, IDataset dataset)
        {
            _evento = evento;
            _dataset = dataset;
        }

        [HttpPost("RegistrarEvento")]
        public IActionResult RegistrarEvento([FromForm] EventoDTO eventoDto, IFormFile audio, List<IFormFile>? imagenes)
        {
            if (User.IsInRole(RolesSistema.Administrador))
            {
                return Forbid();
            }

            try
            {
                List<ArchivoDTO> imagenesDto = new List<ArchivoDTO>();
                if (imagenes != null)
                {
                    imagenesDto = imagenes.Select(ConvertirArchivo).ToList();
                }

                Evento evento = _evento.RegistrarEvento(eventoDto, ConvertirArchivo(audio), imagenesDto, ObtenerIdUsuario());
                return StatusCode(StatusCodes.Status201Created, new EventoRespuestaDTO(evento));
            }
            catch (ExcepcionNegocio ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = RolesSistema.AdministradorOColaborador)]
        [HttpGet("GetEventos")]
        public IActionResult GetEventos()
        {
            return Ok(_evento.GetEventos().Select(item => new EventoRespuestaDTO(item)));
        }

        [Authorize(Roles = RolesSistema.AdministradorOColaborador)]
        [HttpGet("GetEventoPorId")]
        public IActionResult GetEventoId(int id)
        {
            Evento? evento = _evento.GetEventoPorId(id);
            if (evento == null)
            {
                return NotFound();
            }
            return Ok(new EventoRespuestaDTO(evento));
        }

        [Authorize(Roles = RolesSistema.Administrador)]
        [HttpGet("FiltrarEventos")]
        public IActionResult FiltrarEventos([FromQuery] FiltroEvento filtro)
        {
            return Ok(_evento.FiltrarEventos(filtro).Select(item => new EventoRespuestaDTO(item)));
        }

        [Authorize(Roles = RolesSistema.Administrador)]
        [HttpPut("ValidarEvento")]
        public IActionResult ValidarEvento(int id, bool aprobado, int? aveId)
        {
            Evento? evento = _evento.GetEventoPorId(id);
            if (evento == null)
            {
                return NotFound();
            }

            try
            {
                _evento.ValidarEvento(evento, aprobado, aveId);
                return Ok(new EventoRespuestaDTO(evento));
            }
            catch (ExcepcionNegocio ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = RolesSistema.Administrador)]
        [HttpGet("ExportarDataset")]
        public IActionResult ExportarDataset([FromQuery] FiltroEvento filtro)
        {
            byte[] contenido = Encoding.UTF8.GetBytes(_dataset.ExportarCsv(filtro));
            return File(contenido, "text/csv", "dataset_aves.csv");
        }

        private ArchivoDTO ConvertirArchivo(IFormFile archivo)
        {
            using MemoryStream memoria = new MemoryStream();
            archivo.CopyTo(memoria);

            ArchivoDTO archivoDto = new ArchivoDTO();
            archivoDto.NombreOriginal = archivo.FileName;
            archivoDto.Contenido = memoria.ToArray();
            return archivoDto;
        }
    }
}

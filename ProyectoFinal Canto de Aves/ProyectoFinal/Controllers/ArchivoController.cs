using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelos;
using Servicios.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchivoController : ControllerBase
    {
        private readonly IArchivo _archivo;

        public ArchivoController(IArchivo archivo)
        {
            _archivo = archivo;
        }

        [Authorize(Roles = RolesSistema.AdministradorOColaborador)]
        [HttpGet("Reproducir")]
        public IActionResult Reproducir(int id)
        {
            Archivo? archivo = _archivo.GetArchivoPorId(id);
            if (archivo == null)
            {
                return NotFound();
            }
            return File(archivo.Contenido, archivo.TipoContenido(), enableRangeProcessing: true);
        }
    }
}

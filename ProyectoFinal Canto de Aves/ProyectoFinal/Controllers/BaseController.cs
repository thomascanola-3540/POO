using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected int? ObtenerIdUsuario()
        {
            string? id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return id != null ? int.Parse(id) : null;
        }
    }
}

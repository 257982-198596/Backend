using LogicaNegocio.InterfacesRepositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIGestionCobros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificacionesController : ControllerBase
    {

        public IRepositorioNotificaciones RepoNotificaciones { get; set; }

        public NotificacionesController(IRepositorioNotificaciones repoNotificaciones)
        {
            RepoNotificaciones = repoNotificaciones;
        }



    }
}

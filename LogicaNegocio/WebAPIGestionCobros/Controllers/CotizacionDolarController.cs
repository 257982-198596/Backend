using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAPIGestionCobros.Servicios;

namespace WebAPIGestionCobros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CotizacionDolarController : Controller
    {
        public IRepositorioCotizacionDolar RepoCotizacionDolar { get; set; }

        private readonly ActualizarCotizacionDolar _actualizarCotizacionDolar;

        public CotizacionDolarController(IRepositorioCotizacionDolar repoCotizacionDolar, ActualizarCotizacionDolar actualizarCotizacionDolar)
        {
            RepoCotizacionDolar = repoCotizacionDolar;
            _actualizarCotizacionDolar = actualizarCotizacionDolar;
        }

        [HttpPost("actualizar")]
        public async Task<IActionResult> ActualizarCotizacion()
        {
            var cotizacion = await _actualizarCotizacionDolar.ObtenerCotizacion(null);
            if (cotizacion != null)
            {
                RepoCotizacionDolar.Add(cotizacion);
                return Ok("Cotización del dólar actualizada manualmente.");
            }
            return BadRequest("No se pudo obtener la cotización del dólar.");
        }

    }
}

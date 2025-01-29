using LogicaAccesoDatos.BaseDatos;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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

        private readonly ILogger<RepositorioCotizacionDolar> logAzure;

        public CotizacionDolarController(IRepositorioCotizacionDolar repoCotizacionDolar, ActualizarCotizacionDolar actualizarCotizacionDolar, ILogger<RepositorioCotizacionDolar> logger)
        {
            RepoCotizacionDolar = repoCotizacionDolar;
            _actualizarCotizacionDolar = actualizarCotizacionDolar;
            logAzure = logger;
        }

        [HttpPost("actualizar")]
        public async Task<IActionResult> ActualizarCotizacion()
        {
            try
            {
                var cotizacion = await _actualizarCotizacionDolar.ObtenerCotizacion(null);
                if (cotizacion != null)
                {
                    RepoCotizacionDolar.Add(cotizacion);
                    return Ok("Cotización del dólar actualizada manualmente.");
                }
                return BadRequest("No se pudo obtener la cotización del dólar.");
            }
            catch (Exception ex)
            {
                logAzure.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }

        }

    }
}

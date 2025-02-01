using LogicaAccesoDatos.BaseDatos;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using WebAPIGestionCobros.Configuration;
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

        private readonly string apiKeyConfig;

        public CotizacionDolarController(IRepositorioCotizacionDolar repoCotizacionDolar, ActualizarCotizacionDolar actualizarCotizacionDolar, ILogger<RepositorioCotizacionDolar> logger, IOptions<ApiSettings> apiSettings)
        {
            RepoCotizacionDolar = repoCotizacionDolar;
            _actualizarCotizacionDolar = actualizarCotizacionDolar;
            logAzure = logger;
            apiKeyConfig = apiSettings.Value.ApiKey;
        }

        private bool EsApiKeyValida()
        {
            if (!Request.Headers.TryGetValue("ApiKey", out var apiKeyHeader))
            {
                return false;
            }

            return apiKeyHeader == apiKeyConfig;
        }

        [HttpPost("actualizar")]
        public async Task<IActionResult> ActualizarCotizacion()
        {
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }

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
                return BadRequest(ex); 
            }

        }

    }
}

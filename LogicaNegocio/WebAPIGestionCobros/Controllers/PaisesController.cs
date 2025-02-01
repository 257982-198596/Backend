using Excepciones;
using LogicaAccesoDatos.BaseDatos;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using WebAPIGestionCobros.Configuration;

namespace WebAPIGestionCobros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaisesController : ControllerBase
    {
        public IRepositorioPaises RepoPaises { get; set; }

        private readonly ILogger<RepositorioPaises> logAzure;

        private readonly string apiKeyConfig;


        public PaisesController(IRepositorioPaises repoPaises, ILogger<RepositorioPaises> logger, IOptions<ApiSettings> apiSettings)
        {
            RepoPaises = repoPaises;
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

        // GET: api/<PaisController>
        [HttpGet]
        public IActionResult Get()
        {
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }

            try
            {
                IEnumerable<Pais> losPaises = RepoPaises.FindAll();
                if (losPaises == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(losPaises);
                }
            }
            catch (PaisException ex)
            {
                logAzure.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                logAzure.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }


        }
    }
}

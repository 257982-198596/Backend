using Excepciones;
using LogicaAccesoDatos.BaseDatos;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using WebAPIGestionCobros.Configuration;

namespace WebAPIGestionCobros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FrecuenciasController : ControllerBase
    {
        public IRepositorioFrecuencias RepoFrecuencias { get; set; }

        private readonly ILogger<RespositorioFrecuencias> logAzure;

        private readonly string apiKeyConfig;

        public FrecuenciasController(IRepositorioFrecuencias repoFrecuencias, ILogger<RespositorioFrecuencias> logger, IOptions<ApiSettings> apiSettings)
        {
            RepoFrecuencias = repoFrecuencias;
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

        // GET: api/<FrecuenciasController>
        [HttpGet]
        public IActionResult Get()
        {
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }

            try
            {
                IEnumerable<Frecuencia> lasFrecuencias = RepoFrecuencias.FindAll();
                if (lasFrecuencias == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(lasFrecuencias);
                }
            }
            catch (FrecuenciaException ex)
            {
                logAzure.LogError(ex.Message);
                return BadRequest(ex);
            }
            catch (Exception ex)
            {
                logAzure.LogError(ex.Message);
                return BadRequest(ex);
            }


        }
    }
}

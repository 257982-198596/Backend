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
    public class MediosDePagoController : ControllerBase
    {
        public IRepositorioMediosDePago RepoMediosDePago { get; set; }

        private readonly ILogger<RepositorioMediosDePago> logAzure;

        private readonly string apiKeyConfig;

        public MediosDePagoController(IRepositorioMediosDePago repoMediosDePago, ILogger<RepositorioMediosDePago> logger, IOptions<ApiSettings> apiSettings)
        {
            RepoMediosDePago = repoMediosDePago;
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

        // GET: api/<MediosDePagoController>
        [HttpGet]
        public IActionResult Get()
        {
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }

            try
            {
                IEnumerable<MedioDePago> losMediosDePago = RepoMediosDePago.FindAll();
                if (losMediosDePago == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(losMediosDePago);
                }
            }
            catch (MedioDePagoException ex)
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

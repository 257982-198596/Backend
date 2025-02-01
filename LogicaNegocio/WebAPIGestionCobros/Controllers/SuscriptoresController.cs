using Excepciones;
using LogicaAccesoDatos.BaseDatos;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using WebAPIGestionCobros.Configuration;

namespace WebAPIGestionCobros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuscriptoresController : ControllerBase
    {
        public IRepositorioSuscriptores RepoSuscriptores { get; set; }

        private readonly ILogger<RepositorioSuscriptores> logAzure;

        private readonly string apiKeyConfig;

        public SuscriptoresController(IRepositorioSuscriptores repoSuscriptores, ILogger<RepositorioSuscriptores> logger, IOptions<ApiSettings> apiSettings)
        {
            RepoSuscriptores = repoSuscriptores;
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

        // POST api/<SuscriptoresController>
        [HttpPost]
        public IActionResult Post([FromBody] Suscriptor nuevo)
        {
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }

            try
            {
                if (nuevo.PaisId != null && nuevo.PaisId != 0)
                {
                    nuevo.Validar();
                    RepoSuscriptores.Add(nuevo);
                }
                else
                {
                    throw new SuscriptorException("Debe seleccionar un pais para el suscriptor");
                }
            }
            catch (SuscriptorException e)
            {
                logAzure.LogError(e.Message);
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
                logAzure.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

            return Created("api/suscriptores/" + nuevo.Id, nuevo);
        }

        // PUT api/<SuscriptoresController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Suscriptor actualizado)
        {
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }

            try
            {
                if (actualizado.PaisId != null && actualizado.PaisId != 0)
                {
                    actualizado.Id = id;
                    actualizado.Validar();
                    RepoSuscriptores.Update(actualizado);
                }
                else
                {
                    throw new SuscriptorException("Debe seleccionar un pais para el suscriptor");
                }
            }
            catch (SuscriptorException e)
            {
                logAzure.LogError(e.Message);
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
                logAzure.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        // GET api/<SuscriptoresController>/5
        [HttpGet("{idUsuario}")]
        public IActionResult GetSuscriptorPorIdUsuario(int idUsuario)
        {
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }

            try
            {
                Suscriptor suscriptor = RepoSuscriptores.FindByIdUsuario(idUsuario);
                return Ok(suscriptor);
            }
            catch (SuscriptorException e)
            {
                logAzure.LogError(e.Message);
                return NotFound(e.Message);
            }
            catch (Exception ex)
            {
                logAzure.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}

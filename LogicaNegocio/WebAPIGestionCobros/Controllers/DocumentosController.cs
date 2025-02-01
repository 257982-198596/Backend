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
    public class DocumentosController : ControllerBase
    {

        public IRepositorioDocumentos RepoDocumentos { get; set; }

        private readonly ILogger<RepositorioDocumentos> logAzure;

        private readonly string apiKeyConfig;

        public DocumentosController(IRepositorioDocumentos repoDocumentos, ILogger<RepositorioDocumentos> logger, IOptions<ApiSettings> apiSettings)
        {
            RepoDocumentos = repoDocumentos;
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

        [HttpGet]
        public IActionResult Get()
        {
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }

            try
            {
                IEnumerable<Documento> losTiposDocumentos = RepoDocumentos.FindAll();
                if (losTiposDocumentos == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(losTiposDocumentos);
                }
            }
            catch (DocumentoException ex)
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

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
using System.Linq;
using WebAPIGestionCobros.Configuration;

namespace WebAPIGestionCobros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        public IRepositorioCategorias RepoCategorias { get; set; }

        private readonly ILogger<RepositorioCategorias> logAzure;

        private readonly string apiKeyConfig;

        public CategoriasController(IRepositorioCategorias repoCategorias, ILogger<RepositorioCategorias> logger, IOptions<ApiSettings> apiSettings)
        {
            RepoCategorias = repoCategorias;
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
                IEnumerable<Categoria> lasCategorias = RepoCategorias.FindAll();
                if (lasCategorias == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(lasCategorias);
                }
            }
            catch (CategoriaException ex)
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

        [HttpGet("suscriptor/{suscriptorId}")]
        public IActionResult GetBySuscriptorId(int suscriptorId)
        {
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }
            try
            {
                IEnumerable<Categoria> categorias = RepoCategorias.FindBySuscriptorId(suscriptorId);
                if (categorias == null)
                {
                    return NotFound();
                }
                return Ok(categorias);
            }
            catch (CategoriaException ex)
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

        // GET api/<CategoriasController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int? id)
        {
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }

            if (id == null || id == 0)
            {
                return BadRequest();
            }
            try
            {
                Categoria laCategoria = RepoCategorias.FindById(id.Value);
                if (laCategoria == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(laCategoria);
                }
            }
            catch (CategoriaException ex)
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

        // POST api/<CategoriasController>
        [HttpPost]
        public IActionResult Post([FromBody] Categoria nueva)
        {
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }
            try
            {
                
                RepoCategorias.Add(nueva);
                return CreatedAtAction(nameof(Get), new { id = nueva.Id }, nueva);
            }
            catch (CategoriaException ex)
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

        // PUT api/<CategoriasController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Categoria aModificar)
        {
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }
            if (id == null)
            {
                return BadRequest();
            }
            try
            {
                aModificar.Id = id;
                RepoCategorias.Update(aModificar);
                return Ok(aModificar);
            }
            catch (CategoriaException ex)
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

        // DELETE api/<CategoriasController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }
            try
            {
                RepoCategorias.Remove(id);
                return NoContent();
            }
            catch (CategoriaException ex)
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

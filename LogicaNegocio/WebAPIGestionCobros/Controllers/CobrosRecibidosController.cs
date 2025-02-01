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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIGestionCobros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CobrosRecibidosController : ControllerBase
    {
        public IRepositorioCobros RepoCobrosRecibidos { get; set; }

        private readonly ILogger<RepositorioCobros> logAzure;

        private readonly ObservadorService _observadorService;

        private readonly string apiKeyConfig;

        public CobrosRecibidosController(IRepositorioCobros repoCobrosRecibidos, ObservadorService observadorService, ILogger<RepositorioCobros> logger, IOptions<ApiSettings> apiSettings)
        {
            RepoCobrosRecibidos = repoCobrosRecibidos;
            _observadorService = observadorService;
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

        // GET: api/<CobrosRecibidosController>
        [HttpGet]
        public IActionResult Get()
        {
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }

            try
            {
                IEnumerable<CobroRecibido> losCobrosRecibidos = RepoCobrosRecibidos.FindAll();

                if (losCobrosRecibidos == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(losCobrosRecibidos);
                }
            }
            catch (CobroRecibidoException ex)
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

        // COBROS DEL SUSCRIPTOR Y SUS CLIENTES
        [HttpGet("suscriptor/{suscriptorId}")]
        public IActionResult GetBySuscriptorId(int suscriptorId)
        {
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }

            try
            {
                IEnumerable<CobroRecibido> cobros = RepoCobrosRecibidos.FindBySuscriptorId(suscriptorId);
                if (cobros == null)
                {
                    return NotFound();
                }
                return Ok(cobros);
            }
            catch (CobroRecibidoException ex)
            {
                logAzure.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                logAzure.LogError(ex.Message);
                return BadRequest(ex);
            }
        }


        // GET api/<CobrosRecibidosController>/5
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
                CobroRecibido elCobro = RepoCobrosRecibidos.FindById(id.Value);
                if (elCobro == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(elCobro);
                }
            }
            catch (CobroRecibidoException e)
            {
                logAzure.LogError(e.Message);
                return BadRequest(e);
            }
            catch (Exception ex)
            {
                logAzure.LogError(ex.Message);
                return BadRequest(ex);
            }
        }

        // POST api/<CobrosRecibidosController>
        [HttpPost]
        public IActionResult Post([FromBody] CobroRecibido nuevo)
        {
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }

            try
            {
                nuevo.Validar();

                RepoCobrosRecibidos.Add(nuevo);
            }
            catch (CobroRecibidoException e)
            {
                logAzure.LogError(e.Message);
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
                logAzure.LogError(ex.Message);
                return BadRequest(ex);
            }

            return Created("api/cobrosrecibidos" + nuevo.Id, nuevo);
        }

        // PUT api/<CobrosRecibidosController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CobroRecibido aModificar)
        {
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }

            try
            {
                if (aModificar.Id != null)
                {
                    aModificar.Id = id;
                    aModificar.Validar();
                    RepoCobrosRecibidos.Update(aModificar);
                    CobroRecibido nuevoCobro = RepoCobrosRecibidos.FindById(aModificar.Id);
                    return Ok(nuevoCobro);
                }
                else
                {
                    return BadRequest("El id del Cobro es obligatorio");
                }
            }
            catch (CobroRecibidoException ex)
            {
                logAzure.LogError(ex.Message);
                return BadRequest(ex);
            }
            catch (Exception e)
            {
                logAzure.LogError(e.Message);
                return StatusCode(500);
            }
        }

        // DELETE api/<CobrosRecibidosController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }

            try
            {
                if (id != null && id != 0)
                {
                    CobroRecibido elCobrooAEliminar =  RepoCobrosRecibidos.FindById(id);

                    if (elCobrooAEliminar != null)
                    {
                        RepoCobrosRecibidos.Remove(id);
                        return NoContent();
                    }
                    else
                    {
                        return BadRequest("No existe Cobro con ese ID");
                    }

                }
                else
                {
                    return BadRequest("El id es obligatorio para eliminar un Cobro");
                }
            }
            catch (CobroRecibidoException ex)
            {
                logAzure.LogError(ex.Message);
                return BadRequest(ex);
            }
            catch (Exception e)
            {
                logAzure.LogError(e.Message);
                return StatusCode(500);
            }
        }

        //FUNCION QUE RECIBE UN SUSCRIPTOR Y UN ANIO Y PASA EL TOTAL COBRADO POR MES EN DICCIONARIO
        [HttpGet("suscriptor/{suscriptorId}/anio/{year}/cobros-por-mes")]
        public IActionResult GetCobrosPorMes(int suscriptorId, int year)
        {
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }

            try
            {
                var resultado = RepoCobrosRecibidos.SumaCobrosPorMes(suscriptorId, year);
                return Ok(resultado);
            }
            catch (CobroRecibidoException ex)
            {
                logAzure.LogError(ex.Message);
                return BadRequest(ex);
            }
            catch (Exception ex)
            {
                logAzure.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        //FUNCION QUE FILTRA POR SERVICIO - RF011
        [HttpGet("suscriptor/{suscriptorId}/anio/{year}/servicio/{servicioId}/cobros-por-mes")]
        public IActionResult GetCobrosPorMesYServicio(int suscriptorId, int year, int servicioId)
        {
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }

            try
            {
                var resultado = RepoCobrosRecibidos.SumaCobrosPorMesYServicio(suscriptorId, year, servicioId);
                return Ok(resultado);
            }
            catch (CobroRecibidoException ex)
            {
                logAzure.LogError(ex.Message);
                return BadRequest(ex);
            }
            catch (Exception ex)
            {
                logAzure.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        //FUNCION QUE FILTRA POR CLIENTE - RF011
        [HttpGet("suscriptor/{suscriptorId}/anio/{year}/cliente/{clienteId}/cobros-por-mes")]
        public IActionResult GetCobrosPorMesYCliente(int suscriptorId, int year, int clienteId)
        {
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }

            try
            {
                var resultado = RepoCobrosRecibidos.SumaCobrosPorMesYCliente(suscriptorId, year, clienteId);
                return Ok(resultado);
            }
            catch (CobroRecibidoException ex)
            {
                logAzure.LogError(ex.Message);
                return BadRequest(ex);
            }
            catch (Exception ex)
            {
                logAzure.LogError(ex.Message);
                return StatusCode(500);
            }
            
        }
    }
    
}

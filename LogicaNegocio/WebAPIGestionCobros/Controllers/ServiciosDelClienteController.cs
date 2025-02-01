using Excepciones;
using LogicaAccesoDatos.BaseDatos;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
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
    public class ServiciosDelClienteController : ControllerBase
    {
        public IRepositorioServiciosDelCliente RepoServiciosDelCliente { get; set; }

        private readonly ILogger<RepositorioServiciosDelCliente> logAzure;

        private readonly string apiKeyConfig;

        public ServiciosDelClienteController(IRepositorioServiciosDelCliente repoServiciosDelClientes, ILogger<RepositorioServiciosDelCliente> logger, IOptions<ApiSettings> apiSettings)
        {
            RepoServiciosDelCliente = repoServiciosDelClientes;
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

        // GET: api/<ServiciosDelClienteController>
        [HttpGet("{idCliente}")]
        public IActionResult GetServiciosDeUnCliente(int idCliente)
        {
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }

            try
            {

                IEnumerable<ServicioDelCliente> losServiciosdelCliente = RepoServiciosDelCliente.ServiciosDeUnCliente(idCliente);
                return Ok(losServiciosdelCliente);
                
               
            }
            catch (ServicioDelClienteException ex)
            {
                logAzure.LogError(ex.Message);
                return BadRequest(ex);
            }
            catch (Exception e)
            {
                logAzure.LogError(e.Message);
                return BadRequest(e);
            }
            
        }

        [HttpGet("servicio/{idServicioDelCliente}")]
        public IActionResult GetServicioDeClientePorId(int idServicioDelCliente)
        {
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }

            if (idServicioDelCliente == 0)
            {
                return BadRequest("El ID del servicio de cliente es inválido.");
            }
            try
            {
                ServicioDelCliente elServicioDeCliente = RepoServiciosDelCliente.FindById(idServicioDelCliente);
                if (elServicioDeCliente == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(elServicioDeCliente);
                }
            }
            catch (ServicioDelClienteException e)
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

        // GET: api/<ServiciosDelClienteController>
        [HttpGet("activos/{idCliente}")]
        public IActionResult GetServiciosActivosDeUnCliente(int idCliente)
        {
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }

            try
            {
                if (idCliente != null || idCliente != 0)
                {
                    IEnumerable<ServicioDelCliente> losServiciosdelCliente = RepoServiciosDelCliente.ServiciosActivosDeUnCliente(idCliente);
                    return Ok(losServiciosdelCliente);
                }
                else
                {
                    return BadRequest("El ID del cliente es inválido.");
                }
                


            }
            catch (ServicioDelClienteException ex)
            {
                logAzure.LogError(ex.Message);
                return BadRequest(ex);
            }
            catch (Exception e)
            {
                logAzure.LogError(e.Message);
                return BadRequest(e);
            }

        }

        // GET: api/<ServiciosDelClienteController>
        [HttpGet("pagos/{idCliente}")]
        public IActionResult GetServiciosPagosDeUnCliente(int idCliente)
        {
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }

            try
            {
                if (idCliente != null || idCliente != 0)
                {
                    IEnumerable<ServicioDelCliente> losServiciosdelCliente = RepoServiciosDelCliente.ServiciosPagosDeUnCliente(idCliente);
                    return Ok(losServiciosdelCliente);
                }
                else
                {
                    return BadRequest("El ID del cliente es inválido.");
                }



            }
            catch (ServicioDelClienteException ex)
            {
                logAzure.LogError(ex.Message);
                return BadRequest(ex);
            }
            catch (Exception e)
            {
                logAzure.LogError(e.Message);
                return BadRequest(e);
            }

        }

        // GET api/<ServiciosDelClienteController>/5
        [HttpGet("{idCliente}/{idServicioDelCliente}")]
        public IActionResult Get(int idCliente, int idServicioDelCliente)
        {
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }

            if (idCliente == null || idCliente == 0 || idServicioDelCliente == null || idServicioDelCliente == 0)
            {
                return BadRequest();
            }
            try
            {
                ServicioDelCliente elServiciodeCliente = RepoServiciosDelCliente.FindById(idServicioDelCliente);
                if (elServiciodeCliente == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(elServiciodeCliente);
                }
            }
            catch (ServicioDelClienteException e)
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

        // POST api/<ServiciosDelClienteController>
        [HttpPost]
        public IActionResult Post([FromBody] ServicioDelCliente nuevo)
        {
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }

            try
            {
                nuevo.Validar();
                RepoServiciosDelCliente.Add(nuevo);
            }
            catch (ServicioDelClienteException e)
            {
                logAzure.LogError(e.Message);
                return BadRequest(e);
            }
            catch (Exception ex)
            {
                logAzure.LogError(ex.Message);
                return BadRequest(ex);
            }

            return Created("api/serviciosdelcliente" + nuevo.Id, nuevo);

        }

        // PUT api/<ServiciosDelClienteController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ServicioDelCliente aModificar)
        {
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }

            try
            {
                if (id != null || id != 0)
                {
                    aModificar.Id = id;
                    aModificar.Validar();
                    RepoServiciosDelCliente.Update(aModificar);
                    return Ok(aModificar);
                }
                else
                {
                    return BadRequest("El id del servicio de cliente es obligatorio");
                }
            }
            catch (ServicioDelClienteException ex)
            {
                logAzure.LogError(ex.Message);
                return BadRequest(ex);
            }
            catch (Exception e)
            {
                logAzure.LogError(e.Message);
                return BadRequest(e);
            }
        }

        // DELETE api/<ServiciosDelClienteController>/5
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
                    ServicioDelCliente elServicioDeClienteAEliminar = RepoServiciosDelCliente.FindById(id);
                    if (elServicioDeClienteAEliminar != null)
                    {
                        RepoServiciosDelCliente.Remove(id);
                        return NoContent();
                    }
                    else
                    {
                        return BadRequest("No existe servicio de cliente con ese ID");
                    }

                }
                else
                {
                    return BadRequest("El id es obligatorio para eliminar un servicio de cliente");
                }
            }
            catch (ServicioDelClienteException ex)
            {
                logAzure.LogError(ex.Message);
                return BadRequest(ex);
            }
            catch (Exception e)
            {
                logAzure.LogError(e.Message);
                return BadRequest(e);
            }
        }

        [HttpGet("proximo-vencimiento/{idCliente}")]
        public IActionResult ObtenerProximoServicioActivoAVencerse(int idCliente)
        {
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }

            try
            {
                ServicioDelCliente proximoServicio = RepoServiciosDelCliente.ObtenerProximoServicioActivoAVencerse(idCliente);
                if (proximoServicio == null)
                {
                    return NotFound(new { Mensaje = "No se encontraron servicios próximos a vencerse para el cliente." });
                }
                return Ok(new { ClienteId = idCliente, Servicio = proximoServicio, FechaVencimiento = proximoServicio.FechaVencimiento });
            }
            catch (ServicioDelClienteException ex)
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


        [HttpGet("ingresos-proximos-365-dias/{idCliente}")]
        public IActionResult CalcularIngresosProximos365Dias(int idCliente)
        {
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }

            try
            {
                decimal totalIngresos = RepoServiciosDelCliente.CalcularIngresosProximos365Dias(idCliente);
                return Ok(new { ClienteId = idCliente, TotalIngresos = totalIngresos });
            }
            catch (ServicioDelClienteException ex)
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

        //no es solo servicios activos - a corregir
        [HttpGet("activos-suscriptor/{idSuscriptor}")]
        public IActionResult GetServiciosDeClientesDeUnSuscriptor(int idSuscriptor)
        {
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }

            try
            {
                IEnumerable<ServicioDelCliente> servicios = RepoServiciosDelCliente.ServiciosDeClientesDeUnSuscriptor(idSuscriptor);
                return Ok(servicios);
            }
            catch (ServicioDelClienteException ex)
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

        [HttpGet("activos-suscriptor-vencen-este-mes/{idSuscriptor}")]
        public IActionResult GetServiciosActivosDeClientesDeUnSuscriptorQueVencenEsteMes(int idSuscriptor)
        {
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }

            try
            {
                var serviciosQueVencenEsteMes = RepoServiciosDelCliente.ServiciosDeClientesDeUnSuscriptorQueVencenEsteMes(idSuscriptor);
                return Ok(serviciosQueVencenEsteMes);
            }
            catch (ServicioDelClienteException ex)
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

        // INDICADORES REPORTE VENCIMIENTOS DEL MES CORRIENTE
        [HttpGet("indicadores-vencimientos-mes/{idSuscriptor}")]
        public IActionResult GetIndicadoresServiciosVencenEsteMes(int idSuscriptor)
        {
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }

            try
            {
                Dictionary<string, decimal> indicadores = RepoServiciosDelCliente.ObtenerIndicadoresServiciosVencenEsteMes(idSuscriptor);
                return Ok(indicadores);
            }
            catch (ServicioDelClienteException ex)
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

        [HttpPost("cambiar-estado-servicios-del-cliente-a-vencidos")]
        public IActionResult MarcarServiciosComoVencidos()
        {
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }

            try
            {
                IEnumerable<ServicioDelCliente> serviciosVencidos = RepoServiciosDelCliente.MarcarServiciosComoVencidos();
                return Ok(serviciosVencidos);
            }
            catch (ServicioDelClienteException ex)
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

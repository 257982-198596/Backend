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
    public class NotificacionesController : ControllerBase
    {

        public IRepositorioNotificaciones RepoNotificaciones { get; set; }

        private readonly ILogger<RepositorioNotificaciones> logAzure;

        private readonly string apiKeyConfig;

        public NotificacionesController(IRepositorioNotificaciones repoNotificaciones, ILogger<RepositorioNotificaciones> logger, IOptions<ApiSettings> apiSettings)
        {
            RepoNotificaciones = repoNotificaciones;
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
                IEnumerable<Notificacion> lasNotificaciones = RepoNotificaciones.FindAll();
                if (lasNotificaciones == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(lasNotificaciones);
                }
            }
            catch (NotificacionException ex)
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

        //NOTIFICACIONES DE UN SUSCRIPTOR
        [HttpGet("suscriptor/{suscriptorId}")]
        public IActionResult GetBySuscriptorId(int suscriptorId)
        {
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }

            try
            {
                IEnumerable<Notificacion> notificaciones = RepoNotificaciones.FindBySuscriptorId(suscriptorId);
                if (notificaciones == null)
                {
                    return NotFound();
                }
                return Ok(notificaciones);
            }
            catch (NotificacionException ex)
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


        // GET api/<NotificacionesController>/5
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
                Notificacion laNotificacion = RepoNotificaciones.FindById(id.Value);
                if (laNotificacion == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(laNotificacion);
                }
            }
            catch (NotificacionException e)
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

        // POST api/<NotificacionesController>
        [HttpPost]
            public IActionResult Post([FromBody] Notificacion nuevaNotificacion)
            {
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }
            try
                {

                    RepoNotificaciones.Add(nuevaNotificacion);
                }
                catch (NotificacionException e)
                {
                    logAzure.LogError(e.Message);
                    return BadRequest(e.Message);
                }
                catch (Exception ex)
                {
                    logAzure.LogError(ex.Message);
                    return BadRequest(ex);
                }

                return Created("api/notificaciones" + nuevaNotificacion.Id, nuevaNotificacion);
            }


        // PUT api/<ServiciosController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Notificacion aModificar)
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

                    RepoNotificaciones.Update(aModificar);
                    return Ok(aModificar);
                }
                else
                {
                    return BadRequest("El id del servicio es obligatorio");
                }
            }
            catch (NotificacionException ex)
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
                    Notificacion laNotificacionAEliminar = RepoNotificaciones.FindById(id);
                    if (laNotificacionAEliminar != null)
                    {
                        RepoNotificaciones.Remove(id);
                        return NoContent();
                    }
                    else
                    {
                        return BadRequest("No existe Notificacion con ese ID");
                    }

                }
                else
                {
                    return BadRequest("El id es obligatorio para eliminar una notificacion");
                }
            }
            catch (NotificacionException ex)
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

        [HttpPost("enviar")]
        public IActionResult EnviarRecordatorio([FromBody] Notificacion nuevaNotificacion)
        {
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }
            try
            {
                

                RepoNotificaciones.Add(nuevaNotificacion);
                return Ok(nuevaNotificacion);
            }
            catch (NotificacionException ex)
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

        [HttpPost("procesar-notificaciones-de-vencimientos")]
        public IActionResult ProcesarNotificacionesVencimientos()
        {
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }
            try
              {
                IEnumerable<Notificacion> notificacionesGeneradas = RepoNotificaciones.GenerarNotificacionesPendientes();
                if (notificacionesGeneradas == null)
                {
                    return Ok("No se encontraron notificaciones pendientes para procesar.");
                }
                return Ok(new { Mensaje = "Notificaciones procesadas con éxito.", Notificaciones = notificacionesGeneradas });
            }
            catch (NotificacionException ex)
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

        [HttpGet("cantidad-notificaciones/{clienteId}")]
        public IActionResult ObtenerCantidadNotificacionesEnviadas(int clienteId)
        {
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }
            try
            {
                DateTime fechaHaceTreintaDias = DateTime.Now.AddDays(-30);
                int cantidadNotificaciones = RepoNotificaciones.ContarNotificacionesEnviadas(clienteId, fechaHaceTreintaDias);
                return Ok(new { ClienteId = clienteId, CantidadNotificaciones = cantidadNotificaciones });
            }
            catch (NotificacionException ex)
            {
                logAzure.LogError(ex.Message);
                return BadRequest(ex);
            }
            catch (Exception ex)
            {
                logAzure.LogError(ex.Message);
                return StatusCode(500, new { Mensaje = "Error interno del servidor.", Error = ex.Message });
            }
        }

        //NOTIFICACIONES POR MES DE UN AÑO DADO (DEL SUSCRIPTOR)
        [HttpGet("suscriptor/{suscriptorId}/anio/{year}/notificaciones-por-mes")]
        public IActionResult GetNotificacionesPorMes(int suscriptorId, int year)
        {
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }
            try
            {
                Dictionary<int, decimal> notificacionesPorMes = RepoNotificaciones.CantidadNotificacionesPorMesaDelSuscriptorId(suscriptorId, year);
                return Ok(notificacionesPorMes);
            }
            catch (NotificacionException ex)
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


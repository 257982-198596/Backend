using Excepciones;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace WebAPIGestionCobros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificacionesController : ControllerBase
    {

        public IRepositorioNotificaciones RepoNotificaciones { get; set; }

        public NotificacionesController(IRepositorioNotificaciones repoNotificaciones)
        {
            RepoNotificaciones = repoNotificaciones;
        }

        [HttpGet]
        public IActionResult Get()
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

        // GET api/<NotificacionesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int? id)
        {
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
                return BadRequest(e);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST api/<NotificacionesController>
        [HttpPost]
            public IActionResult Post([FromBody] Notificacion nuevaNotificacion)
            {
                try
                {

                    RepoNotificaciones.Add(nuevaNotificacion);
                }
                catch (NotificacionException e)
                {
                    return BadRequest(e.Message);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }

                return Created("api/notificaciones" + nuevaNotificacion.Id, nuevaNotificacion);
            }


        // PUT api/<ServiciosController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Notificacion aModificar)
        {
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
                return BadRequest(ex);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
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
                return BadRequest(ex);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
    }


    
}

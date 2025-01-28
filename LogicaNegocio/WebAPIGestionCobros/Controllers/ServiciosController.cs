using Excepciones;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIGestionCobros.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ServiciosController : ControllerBase
    {
        public IRepositorioServicios RepoServicios { get; set; }

        public ServiciosController(IRepositorioServicios repoServicios)
        {
            RepoServicios = repoServicios;
        }


        // GET: api/<ServiciosController>
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Servicio> losServicios = RepoServicios.FindAll();
            if (losServicios == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(losServicios);
            }
        }

        // GET api/<ServiciosController>/suscriptor/5
        [HttpGet("suscriptor/{suscriptorId}")]
        public IActionResult GetServiciosBySuscriptorId(int suscriptorId)
        {
            try
            {
                IEnumerable<Servicio> losServicios = RepoServicios.FindAllBySuscriptorId(suscriptorId);
                if (losServicios == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(losServicios);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/<ServiciosController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            try
            {
                Servicio elServicio = RepoServicios.FindById(id.Value);
                if (elServicio == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(elServicio);
                }
            }
            catch (ServicioException e)
            {
                return BadRequest(e);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST api/<ServiciosController>
        [HttpPost]
        public IActionResult Post([FromBody] Servicio nuevo)
        {
            try
            {
                nuevo.Validar();
                RepoServicios.Add(nuevo);
            }
            catch (ServicioException e)
            {
                return BadRequest(e);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return Created("api/servicios" + nuevo.Id, nuevo);
        }

        // PUT api/<ServiciosController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Servicio aModificar)
        {
            try
            {
                if (aModificar.Id != null)
                {
                    aModificar.Id = id;
                    aModificar.Validar();
                    RepoServicios.Update(aModificar);
                    return Ok(aModificar);
                }
                else
                {
                    return BadRequest("El id del servicio es obligatorio");
                }
            }
            catch (ServicioException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        // DELETE api/<ServiciosController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id != null && id != 0)
                {
                    Servicio elServicioAEliminar = RepoServicios.FindById(id);
                    if (elServicioAEliminar != null)
                    {
                        RepoServicios.Remove(id);
                        return NoContent();
                    }
                    else
                    {
                        return BadRequest("No existe Servicio con ese ID");
                    }

                }
                else
                {
                    return BadRequest("El id es obligatorio para eliminar un servicio");
                }
            }
            catch (ServicioException ex)
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

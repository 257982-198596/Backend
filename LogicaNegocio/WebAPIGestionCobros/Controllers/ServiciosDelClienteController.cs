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
    public class ServiciosDelClienteController : ControllerBase
    {
        public IRepositorioServiciosDelCliente RepoServiciosDelCliente { get; set; }

        public ServiciosDelClienteController(IRepositorioServiciosDelCliente repoServiciosDelClientes)
        {
            RepoServiciosDelCliente = repoServiciosDelClientes;
        }

        // GET: api/<ServiciosDelClienteController>
        [HttpGet("{idCliente}")]
        public IActionResult GetServiciosDeUnCliente(int idCliente)
        {
            try
            {

                IEnumerable<ServicioDelCliente> losServiciosdelCliente = RepoServiciosDelCliente.ServiciosDeUnCliente(idCliente);
                return Ok(losServiciosdelCliente);
                
               
            }
            catch (ServicioDelClienteException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            
        }

        // GET: api/<ServiciosDelClienteController>
        [HttpGet("activos/{idCliente}")]
        public IActionResult GetServiciosActivosDeUnCliente(int idCliente)
        {
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
                return BadRequest(ex);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

        // GET: api/<ServiciosDelClienteController>
        [HttpGet("pagos/{idCliente}")]
        public IActionResult GetServiciosPagosDeUnCliente(int idCliente)
        {
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
                return BadRequest(ex);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }

        // GET api/<ServiciosDelClienteController>/5
        [HttpGet("{idCliente}/{idServicioDelCliente}")]
        public IActionResult Get(int idCliente, int idServicioDelCliente)
        {
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
                return BadRequest(e);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST api/<ServiciosDelClienteController>
        [HttpPost]
        public IActionResult Post([FromBody] ServicioDelCliente nuevo)
        {
            try
            {
                nuevo.Validar();
                RepoServiciosDelCliente.Add(nuevo);
            }
            catch (ServicioDelClienteException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return Created("api/serviciosdelcliente" + nuevo.Id, nuevo);

        }

        // PUT api/<ServiciosDelClienteController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ServicioDelCliente aModificar)
        {
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
                return BadRequest(ex);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        // DELETE api/<ServiciosDelClienteController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
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
                return BadRequest(ex);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
    }
}

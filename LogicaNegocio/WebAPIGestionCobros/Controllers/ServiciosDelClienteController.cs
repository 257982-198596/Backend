using Excepciones;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

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
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<ServicioDelCliente> losServiciosdeLosClientes = RepoServiciosDelCliente.FindAll();
                if (losServiciosdeLosClientes == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(losServiciosdeLosClientes);
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
        [HttpGet("{id}")]
        public IActionResult Get(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            try
            {
                ServicioDelCliente elServiciodeCliente = RepoServiciosDelCliente.FindById(id.Value);
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
                if (aModificar.Id != null && aModificar.Id != 0)
                {
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

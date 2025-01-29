using Excepciones;
using LogicaAccesoDatos.BaseDatos;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIGestionCobros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        public IRepositorioClientes RepoClientes { get; set; }

        private readonly ILogger<RepositorioClientes> logAzure;

        public ClientesController(IRepositorioClientes repoClientes, ILogger<RepositorioClientes> logger)
        {
            RepoClientes = repoClientes;
            logAzure = logger;
        }


        // GET: api/<ClientesController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<Cliente> losClientes = RepoClientes.FindAll();
                if (losClientes == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(losClientes);
                }
            }
            catch (ClienteException ex)
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

        // GET api/<ClientesController>/suscriptor/5
        [HttpGet("suscriptor/{suscriptorId}")]
        public IActionResult GetClientesBySuscriptorId(int suscriptorId)
        {
            try
            {
                IEnumerable<Cliente> losClientes = RepoClientes.FindAllBySuscriptorId(suscriptorId);
                if (losClientes != null)
                {
                    return Ok(losClientes);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (ClienteException ex)
            {
                logAzure.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                logAzure.LogError(ex.Message);
                return StatusCode(500, ex);
            }
        }

        // GET api/<ClientesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int? id)
        {
            if(id == null || id == 0)
            {
                return BadRequest();
            }
            try
            {
                Cliente elCliente = RepoClientes.FindById(id.Value);
                if (elCliente == null)
                {
                    return NotFound();
                }else
                {
                    return Ok(elCliente);
                }
            }
            catch (ClienteException e)
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

        // POST api/<ClientesController>
        [HttpPost]
        public IActionResult Post([FromBody] Cliente nuevo)
        {
            try
            {
                if(nuevo.PaisId != null && nuevo.PaisId != 0)
                {
                    if (nuevo.DocumentoId != null && nuevo.DocumentoId != 0)
                    {
                        nuevo.Validar();
                        RepoClientes.Add(nuevo);
                    }
                    else
                    {
                        throw new ClienteException("Debe seleccionar un tipo de documento para el cliente");
                    }

                }
                else
                {
                    throw new ClienteException("Debe seleccionar un pais para el cliente");
                }
                
            }
            catch (ClienteException e)
            {
                logAzure.LogError(e.Message);
                return BadRequest(e);
            }
            catch(Exception ex)
            {
                logAzure.LogError(ex.Message);
                return BadRequest(ex);
            }

            return Created("api/clientes" + nuevo.Id, nuevo);
        }

        // PUT api/<ClientesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Cliente aModificar)
        {
            try
            {
               if(aModificar.Id != null)
                {
                    aModificar.Id = id;
                    aModificar.Validar();
                    RepoClientes.Update(aModificar);
                    return Ok(aModificar);
                }
                else
                {
                    return BadRequest("El id del usuario es obligatorio");
                }
            }
            catch (ClienteException ex)
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

        // DELETE api/<ClientesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id != null && id != 0)
                {
                    Cliente elClienteAEliminar = RepoClientes.FindById(id);
                    if (elClienteAEliminar != null)
                    {
                        RepoClientes.Remove(id);
                        return NoContent();
                    }
                    else
                    {
                        return BadRequest("No existe cliente con ese ID");
                    }

                }
                else
                {
                    return BadRequest("El id es obligatorio para eliminar un cliente");
                }
            }
            catch (ClienteException ex)
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

        // PUT api/<ClientesController>/habilitar/5
        [HttpPut("habilitar/{id}")]
        public IActionResult Habilitar(int id)
        {
            try
            {
                RepoClientes.HabilitarCliente(id);
                return Ok();
            }
            catch (ClienteException ex)
            {
                logAzure.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception e)
            {
                logAzure.LogError(e.Message);
                return StatusCode(500, e.Message);
            }
        }

        // PUT api/<ClientesController>/deshabilitar/5
        [HttpPut("deshabilitar/{id}")]
        public IActionResult Deshabilitar(int id)
        {
            try
            {
                RepoClientes.DeshabilitarCliente(id);
                return Ok();
            }
            catch (ClienteException ex)
            {
                logAzure.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Exception e)
            {
                logAzure.LogError(e.Message);
                return StatusCode(500, e.Message);
            }
        }
    }
}

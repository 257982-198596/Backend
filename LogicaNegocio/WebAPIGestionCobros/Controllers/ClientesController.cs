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
    public class ClientesController : ControllerBase
    {
        public IRepositorioClientes RepoClientes { get; set; }

        private readonly ILogger<RepositorioClientes> logAzure;

        private readonly string apiKeyConfig;

        public ClientesController(IRepositorioClientes repoClientes, ILogger<RepositorioClientes> logger, IOptions<ApiSettings> apiSettings)
        {
            RepoClientes = repoClientes;
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

        // GET: api/<ClientesController>
        [HttpGet]
        public IActionResult Get()
        {
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }
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
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }
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
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }
            try
            {
                if(nuevo.PaisId != null && nuevo.PaisId != 0)
                {
                    if (nuevo.DocumentoId != null && nuevo.DocumentoId != 0)
                    {
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
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }
            try
            {
               if(aModificar.Id != null)
                {
                    aModificar.Id = id;
                    //aModificar.Validar();
                    RepoClientes.Update(aModificar);
                    Cliente clienteActualizado = RepoClientes.FindById(id);
                    return Ok(clienteActualizado);
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
                return BadRequest(e);
            }

        }

        [HttpPut("actualizar-perfil/{id}")]
        public IActionResult PutPerfilCliente(int id, [FromBody] Cliente aModificar)
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
                    //aModificar.Validar();
                    Cliente clienteActualizado = RepoClientes.UpdatePerfilCliente(aModificar);
                    
                    return Ok(clienteActualizado);
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
                return BadRequest(e);
            }

        }

        // DELETE api/<ClientesController>/5
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
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }
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
            if (!EsApiKeyValida())
            {
                return Unauthorized("API Key inválida o no proporcionada.");
            }
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

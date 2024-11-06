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
    public class ClientesController : ControllerBase
    {
        public IRepositorioClientes RepoClientes { get; set; }

        public ClientesController(IRepositorioClientes repoClientes)
        {
            RepoClientes = repoClientes;
        }


        // GET: api/<ClientesController>
        [HttpGet]
        public IActionResult Get()
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
                return BadRequest(e);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST api/<ClientesController>
        [HttpPost]
        public IActionResult Post([FromBody] Cliente nuevo)
        {
            try
            {
                nuevo.Validar();
                RepoClientes.Add(nuevo);
            }
            catch (ClienteException e)
            {
                return BadRequest(e);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }

            return Created("api/clientes" + nuevo.Id, nuevo);
        }

        // PUT api/<ClientesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ClientesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

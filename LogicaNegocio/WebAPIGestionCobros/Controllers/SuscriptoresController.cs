using Excepciones;
using LogicaAccesoDatos.BaseDatos;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace WebAPIGestionCobros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuscriptoresController : ControllerBase
    {
        public IRepositorioSuscriptores RepoSuscriptores { get; set; }

        private readonly ILogger<RepositorioSuscriptores> logAzure;

        public SuscriptoresController(IRepositorioSuscriptores repoSuscriptores, ILogger<RepositorioSuscriptores> logger)
        {
            RepoSuscriptores = repoSuscriptores;
            logAzure = logger;
        }

        // POST api/<SuscriptoresController>
        [HttpPost]
        public IActionResult Post([FromBody] Suscriptor nuevo)
        {
            try
            {
                if (nuevo.PaisId != null && nuevo.PaisId != 0)
                {
                    nuevo.Validar();
                    RepoSuscriptores.Add(nuevo);
                }
                else
                {
                    throw new SuscriptorException("Debe seleccionar un pais para el suscriptor");
                }
            }
            catch (SuscriptorException e)
            {
                logAzure.LogError(e.Message);
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
                logAzure.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

            return Created("api/suscriptores/" + nuevo.Id, nuevo);
        }

        // PUT api/<SuscriptoresController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Suscriptor actualizado)
        {
            try
            {
                if (actualizado.PaisId != null && actualizado.PaisId != 0)
                {
                    actualizado.Id = id;
                    actualizado.Validar();
                    RepoSuscriptores.Update(actualizado);
                }
                else
                {
                    throw new SuscriptorException("Debe seleccionar un pais para el suscriptor");
                }
            }
            catch (SuscriptorException e)
            {
                logAzure.LogError(e.Message);
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
                logAzure.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        // GET api/<SuscriptoresController>/5
        [HttpGet("{idUsuario}")]
        public IActionResult GetSuscriptorPorIdUsuario(int idUsuario)
        {
            try
            {
                Suscriptor suscriptor = RepoSuscriptores.FindByIdUsuario(idUsuario);
                return Ok(suscriptor);
            }
            catch (SuscriptorException e)
            {
                logAzure.LogError(e.Message);
                return NotFound(e.Message);
            }
            catch (Exception ex)
            {
                logAzure.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}

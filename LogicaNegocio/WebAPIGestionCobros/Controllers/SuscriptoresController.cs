using Excepciones;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebAPIGestionCobros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuscriptoresController : ControllerBase
    {
        public IRepositorioSuscriptores RepoSuscriptores { get; set; }

        public SuscriptoresController(IRepositorioSuscriptores repoSuscriptores)
        {
            RepoSuscriptores = repoSuscriptores;
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
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
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
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
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
                return NotFound(e.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

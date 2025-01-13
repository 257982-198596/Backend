using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebAPIGestionCobros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        public IRepositorioCategorias RepoCategorias { get; set; }

        public CategoriasController(IRepositorioCategorias repoCategorias)
        {
            RepoCategorias = repoCategorias;
        }



        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Categoria> lasCategorias = RepoCategorias.FindAll();
            if (lasCategorias == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(lasCategorias);
            }

        }

        [HttpGet("suscriptor/{suscriptorId}")]
        public IActionResult GetBySuscriptorId(int suscriptorId)
        {
            try
            {
                IEnumerable<Categoria> categorias = RepoCategorias.FindBySuscriptorId(suscriptorId);
                if (categorias == null)
                {
                    return NotFound();
                }
                return Ok(categorias);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET api/<CategoriasController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int? id)
        {
            if (id == null || id == 0)
            {
                return BadRequest();
            }
            try
            {
                Categoria laCategoria = RepoCategorias.FindById(id.Value);
                if (laCategoria == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(laCategoria);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST api/<CategoriasController>
        [HttpPost]
        public IActionResult Post([FromBody] Categoria nueva)
        {
            try
            {
                nueva.Validar();
                RepoCategorias.Add(nueva);
                return CreatedAtAction(nameof(Get), new { id = nueva.Id }, nueva);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT api/<CategoriasController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Categoria aModificar)
        {
            if (id == null)
            {
                return BadRequest();
            }
            try
            {
                aModificar.Id = id;
                aModificar.Validar();
                RepoCategorias.Update(aModificar);
                return Ok(aModificar);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE api/<CategoriasController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                RepoCategorias.Remove(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}

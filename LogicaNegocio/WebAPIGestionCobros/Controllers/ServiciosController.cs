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
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return Created("api/servicios" + nuevo.Id, nuevo);
        }

        // PUT api/<ServiciosController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ServiciosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

using Excepciones;
using LogicaAccesoDatos.BaseDatos;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace WebAPIGestionCobros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaisesController : ControllerBase
    {
        public IRepositorioPaises RepoPaises { get; set; }

        private readonly ILogger<RepositorioPaises> logAzure;

        public PaisesController(IRepositorioPaises repoPaises, ILogger<RepositorioPaises> logger)
        {
            RepoPaises = repoPaises;
            logAzure = logger;
        }


        // GET: api/<PaisController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<Pais> losPaises = RepoPaises.FindAll();
                if (losPaises == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(losPaises);
                }
            }
            catch (PaisException ex)
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
    }
}

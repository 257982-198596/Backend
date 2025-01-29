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
    public class FrecuenciasController : ControllerBase
    {
        public IRepositorioFrecuencias RepoFrecuencias { get; set; }

        private readonly ILogger<RespositorioFrecuencias> logAzure;

        public FrecuenciasController(IRepositorioFrecuencias repoFrecuencias, ILogger<RespositorioFrecuencias> logger)
        {
            RepoFrecuencias = repoFrecuencias;
            logAzure = logger;
        }


        // GET: api/<FrecuenciasController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<Frecuencia> lasFrecuencias = RepoFrecuencias.FindAll();
                if (lasFrecuencias == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(lasFrecuencias);
                }
            }
            catch (FrecuenciaException ex)
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

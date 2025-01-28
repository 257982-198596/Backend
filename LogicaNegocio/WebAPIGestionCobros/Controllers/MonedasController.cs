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
    public class MonedasController : ControllerBase
    {
        public IRepositorioMonedas RepoMonedas { get; set; }

        private readonly ILogger<RepositorioMonedas> logAzure;
        public MonedasController(IRepositorioMonedas repoMonedas, ILogger<RepositorioMonedas> logger)
        {
            RepoMonedas = repoMonedas;
            logAzure = logger;
        }


        // GET: api/<MonedaController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<Moneda> lasMonedas = RepoMonedas.FindAll();
                if (lasMonedas == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(lasMonedas);
                }
            }
            catch (MonedaException ex)
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

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
    public class MediosDePagoController : ControllerBase
    {
        public IRepositorioMediosDePago RepoMediosDePago { get; set; }

        private readonly ILogger<RepositorioMediosDePago> logAzure;

        public MediosDePagoController(IRepositorioMediosDePago repoMediosDePago, ILogger<RepositorioMediosDePago> logger)
        {
            RepoMediosDePago = repoMediosDePago;
            logAzure = logger;
        }


        // GET: api/<MediosDePagoController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<MedioDePago> losMediosDePago = RepoMediosDePago.FindAll();
                if (losMediosDePago == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(losMediosDePago);
                }
            }
            catch (MedioDePagoException ex)
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

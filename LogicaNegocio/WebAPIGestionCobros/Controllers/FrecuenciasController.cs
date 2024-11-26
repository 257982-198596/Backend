using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebAPIGestionCobros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FrecuenciasController : ControllerBase
    {
        public IRepositorioFrecuencias RepoFrecuencias { get; set; }

        public FrecuenciasController(IRepositorioFrecuencias repoFrecuencias)
        {
            RepoFrecuencias = repoFrecuencias;
        }


        // GET: api/<FrecuenciasController>
        [HttpGet]
        public IActionResult Get()
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
    }
}

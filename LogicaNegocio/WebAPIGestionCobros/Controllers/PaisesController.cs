using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebAPIGestionCobros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaisesController : ControllerBase
    {
        public IRepositorioPaises RepoPaises { get; set; }

        public PaisesController(IRepositorioPaises repoPaises)
        {
            RepoPaises = repoPaises;
        }


        // GET: api/<PaisController>
        [HttpGet]
        public IActionResult Get()
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
    }
}

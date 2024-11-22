using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
    }
}

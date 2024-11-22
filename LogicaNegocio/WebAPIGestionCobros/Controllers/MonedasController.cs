using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebAPIGestionCobros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonedasController : ControllerBase
    {
        public IRepositorioMonedas RepoMonedas { get; set; }

        public MonedasController(IRepositorioMonedas repoMonedas)
        {
            RepoMonedas = repoMonedas;
        }


        // GET: api/<MonedaController>
        [HttpGet]
        public IActionResult Get()
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
    }
}

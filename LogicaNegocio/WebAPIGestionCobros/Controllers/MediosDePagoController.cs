using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebAPIGestionCobros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediosDePagoController : ControllerBase
    {
        public IRepositorioMediosDePago RepoMediosDePago { get; set; }

        public MediosDePagoController(IRepositorioMediosDePago repoMediosDePago)
        {
            RepoMediosDePago = repoMediosDePago;
        }


        // GET: api/<MediosDePagoController>
        [HttpGet]
        public IActionResult Get()
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
    }
}

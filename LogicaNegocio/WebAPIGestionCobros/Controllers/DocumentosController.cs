using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebAPIGestionCobros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentosController : ControllerBase
    {

        public IRepositorioDocumentos RepoDocumentos { get; set; }

        public DocumentosController(IRepositorioDocumentos repoDocumentos)
        {
            RepoDocumentos = repoDocumentos;
        }


       
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Documento> losTiposDocumentos = RepoDocumentos.FindAll();
            if (losTiposDocumentos == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(losTiposDocumentos);
            }

        }
    }
}

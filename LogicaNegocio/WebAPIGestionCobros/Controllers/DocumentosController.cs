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
    public class DocumentosController : ControllerBase
    {

        public IRepositorioDocumentos RepoDocumentos { get; set; }

        private readonly ILogger<RepositorioDocumentos> logAzure;

        public DocumentosController(IRepositorioDocumentos repoDocumentos, ILogger<RepositorioDocumentos> logger)
        {
            RepoDocumentos = repoDocumentos;
            logAzure = logger;
        }


       
        [HttpGet]
        public IActionResult Get()
        {
            try
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
            catch (DocumentoException ex)
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

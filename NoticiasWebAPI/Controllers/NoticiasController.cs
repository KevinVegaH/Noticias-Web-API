using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoticiasWebAPI.Models;
using NoticiasWebAPI.Servicios;

namespace NoticiasWebAPI.Controllers
{
    [Route("api/noticias")]
    [ApiController]
    // JwtBearerDefaults.AuthenticationScheme <-- Esquema de autenticación Json Wen Token
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] // <-- Permite el acceso solo al personal autorizado.
    public class NoticiasController : ControllerBase
    {
        private readonly NoticiasServices _NoticiasServicio;

        public NoticiasController(NoticiasServices NoticiasServicio)
        {

            _NoticiasServicio = NoticiasServicio;

        }

        public IActionResult Prueba()
        {

            return Ok("Funciona!");
        }

        [Route("verNoticias")]
        [HttpGet] // No es necesario colocarlo, el sistema lo coloca por defecto.
        public IActionResult verNoticias() {

            var resultado = _NoticiasServicio.verListadoTodasLasNoticias();

            return Ok(resultado);
        }

        [Route("Agregar")]
        [HttpPost] // Agregar
        public IActionResult Agregar([FromBody] Noticia NoticiaAgregar) {

            if (_NoticiasServicio.Agregar(NoticiaAgregar))
            {

                return Ok("Agregado"); // <-- Si la noticia se agrega aparecerá el mensaje de agregado.

            }
            else {

                return BadRequest(); // <-- devuelve un mensaje de error.

            }

            
        }

        [Route("Editar")]
        [HttpPut] // Agregar
        public IActionResult Editar([FromBody] Noticia NoticiaEditar)
        {

            if (_NoticiasServicio.Editar(NoticiaEditar))
            {

                return Ok();

            }
            else {

                return BadRequest();
            }
        }

        // En caso tal de querer pasarle más de un parametro colocamos:
        //  Eliminar/{noticiaID}/{AutorID}

        [Route("Eliminar/{noticiaID}")]
        public IActionResult Eliminar(int noticiaID) {

            if (_NoticiasServicio.Eliminar(noticiaID))
            {

                return Ok();

            }
            else
            {

                return BadRequest();
            }
        }

    }
}
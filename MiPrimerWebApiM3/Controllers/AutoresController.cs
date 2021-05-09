using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiPrimerWebApiM3.Contexts;
using MiPrimerWebApiM3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimerWebApiM3.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController : ControllerBase{

        private readonly ApplicationDbContext context;
        public AutoresController(ApplicationDbContext context) {

            this.context = context;

        }
        /// <summary>
        /// Tra una lista de autores de la BBDD
        /// IEnumerable viene a ser como una Lista de Autores
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<Autor>> Get() {

            return context.Autores.Include(x => x.Libros).ToList();
        }

        [HttpGet("{id}", Name = "ObtenerAutor")]
        public ActionResult<Autor> Get(int id) {

            var autor = context.Autores.Include(x => x.Libros).FirstOrDefault(x => x.id == id);

            //Si el autor no existe devuelve un NotFound
            if(autor == null) {

                return NotFound();
            }

            return autor;

        }

        [HttpPost]
        public ActionResult Post([FromBody] Autor autor) {

            context.Autores.Add(autor);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerAutor", new { id = autor.id }, autor);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Autor value) {

            //Comprobamos que los id son iguales
            if(id != value.id) {
                return BadRequest();
            }

            context.Entry(value).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Autor> Delete(int id) {

            var autor = context.Autores.FirstOrDefault(x => x.id == id);

            if (autor == null) {

                return NotFound();
            }

            context.Autores.Remove(autor);
            context.SaveChanges();
            return autor;
        }
    }
}

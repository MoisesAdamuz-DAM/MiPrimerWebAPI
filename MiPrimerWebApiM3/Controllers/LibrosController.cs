

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiPrimerWebApiM3.Contexts;
using MiPrimerWebApiM3.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MiPrimerWebApiM3.Controllers {
    //Controles predeterminados
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase {
       
        /**Inyeccion de dependencias para obtener acceso a una instancia de ApplicationDbContext**/
        private readonly ApplicationDbContext context;
      
        public LibrosController(ApplicationDbContext context) {

            this.context = context;
        }
        /*******************************************************/

        /*******************************************************************************/
        //Metodos
        /********************************************************************************/

        /// <summary>
        /// Utilizamod Include para incluir la entidad relacionada
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<Libro>> Get() {

            return context.Libros.Include(x => x.Autor).ToList();
        }

        [HttpGet("{id}", Name = "ObtenerLibro")]
        public ActionResult<Libro> Get(int id) {

            var libro = context.Libros.Include(x => x.Autor).FirstOrDefault(x => x.Id == id);

            //Si el autor no existe devuelve un NotFound
            if (libro == null) {

                return NotFound();
            }

            return libro;

        }

        [HttpPost]
        public ActionResult Post([FromBody] Libro libro) {

            context.Libros.Add(libro);
            context.SaveChanges();
            return new CreatedAtRouteResult("ObtenerLibro", new { id = libro.Id }, libro);
        }


        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Libro libro) {

            //Comprobamos que los id son iguales
            if (id != libro.Id) {
                return BadRequest();
            }

            context.Entry(libro).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Libro> Delete(int id) {

            var libro = context.Libros.FirstOrDefault(x => x.Id == id);

            if (libro == null) {

                return NotFound();
            }

            context.Libros.Remove(libro);
            context.SaveChanges();
            return libro;
        }
    }
}

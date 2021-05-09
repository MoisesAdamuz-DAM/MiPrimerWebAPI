using Microsoft.EntityFrameworkCore;
using MiPrimerWebApiM3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiPrimerWebApiM3.Contexts {
    public class ApplicationDbContext : DbContext{

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) {

        }

        /// <summary>
        /// Nombre de la tabla es Autores
        /// Relacionamos la clase Autor con la tabla Autores de la BBDD
        /// Autores copiará las propiedades de la clase autor
        /// La tabla Autores tendrá 2 columnas: id y Nombre
        /// </summary>
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Libro> Libros { get; set; }
    }
}

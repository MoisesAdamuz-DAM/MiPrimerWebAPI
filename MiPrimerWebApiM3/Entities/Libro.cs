using System.ComponentModel.DataAnnotations;

namespace MiPrimerWebApiM3.Entities {
    public class Libro {

        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public int AutorId { get; set; }
        public Autor Autor { get; set; }
    }
}

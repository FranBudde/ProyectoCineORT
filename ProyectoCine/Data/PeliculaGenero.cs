using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoCine.Data
{
    public class PeliculaGenero
    {
        [Key]
        public int IdPeliculaGenero { get; set; }

        [Required]
        public int IdPelicula { get; set; }

        [ForeignKey(nameof(IdPelicula))]
        public Pelicula Pelicula { get; set; }

        [Required]
        public int IdGenero { get; set; }

        [ForeignKey(nameof(IdGenero))]
        public Genero Genero { get; set; }
    }
}

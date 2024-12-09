using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoCine.Models
{
    public class Pelicula
    {

        [Key]
        public int IdPelicula { get; set; }

        [Required, MinLength(3)]
        public string NamePelicula { get; set; }

        [Required, MinLength(2)]
        public int Duracion { get; set; }

        [Required, MinLength(2)]
        public string Idioma { get; set; }

        [Required]
        public bool Subtitulada { get; set; }


        [Required]
        public int IdGenero { get; set; }


        [ForeignKey(nameof(IdGenero))]
        public Genero Genero { get; set; }


        [Required]
        public int IdSala { get; set; }


        [ForeignKey(nameof(IdSala))]
        public Sala Sala { get; set; }

        public ICollection<PeliculaHorario> PeliculaHorarios { get; set; }

        public ICollection<PeliculaGenero> PeliculaGeneros { get; set; }


    }
}

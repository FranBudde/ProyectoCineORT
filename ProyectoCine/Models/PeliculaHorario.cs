using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoCine.Models
{
    public class PeliculaHorario
    {
        [Key]
        public int IdPeliculaHorario { get; set; }

        [Required]
        public int IdPelicula { get; set; }

        [ForeignKey(nameof(IdPelicula))]
        public Pelicula Pelicula { get; set; }

        [Required]
        public int IdHorario { get; set; }

        [ForeignKey(nameof(IdHorario))]
        public Horario Horario { get; set; }
    }
}

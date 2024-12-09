using System.ComponentModel.DataAnnotations;

namespace ProyectoCine.Data
{
    public class Horario
    {
        [Key]
        public int IdHorario { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public TimeOnly Hora { get; set; }

        public ICollection<PeliculaHorario> PeliculaHorarios { get; set; }
    }
}

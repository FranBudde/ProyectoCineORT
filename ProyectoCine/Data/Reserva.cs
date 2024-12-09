using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoCine.Data
{
    public class Reserva
    {
        [Key]
        public int IdReserva { get; set; }

        [Required]
        public int IdUsuario { get; set; }


        [ForeignKey(nameof(IdUsuario))]
        public Usuario Usuario { get; set; }

        [Required]
        public int IdPeliculaHorario { get; set; }

        [ForeignKey(nameof(IdPeliculaHorario))]
        public PeliculaHorario PeliculaHorario { get; set; }
    }
}

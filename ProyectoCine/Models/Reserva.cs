using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoCine.Models
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

        [Required]
        public int IdButaca { get; set; }

        [ForeignKey(nameof(IdButaca))]
        public Butaca Butaca { get; set; }
    }
}

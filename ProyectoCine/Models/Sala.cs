using System.ComponentModel.DataAnnotations;

namespace ProyectoCine.Models
{
    public class Sala
    {

        [Key]
        public int IdSala { get; set; }

        [Required]
        public int Capacidad { get; set; }
    }
}

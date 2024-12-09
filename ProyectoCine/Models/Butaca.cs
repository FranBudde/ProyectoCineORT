using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoCine.Models
{
    public class Butaca
    {
        [Key]
        public int IdButaca { get; set; }

        [Required]
        public bool IsAvailable { get; set; } = true;

        [Required]
        public int numeroButaca { get; set; }

        [Required, MinLength(1)]
        public string Letra { get; set; }

        [Required]
        public int IdSala { get; set; }

        [ForeignKey(nameof(IdSala))]
        public Sala Sala { get; set; }
    }
}

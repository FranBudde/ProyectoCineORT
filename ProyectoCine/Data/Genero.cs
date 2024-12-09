using System.ComponentModel.DataAnnotations;

namespace ProyectoCine.Data
{
    public class Genero
    {

        [Key]
        public int IdGenero { get; set; }

        [Required, MinLength(3)]
        public string TipoGenero { get; set; }

        public ICollection<PeliculaGenero> PeliculaGeneros { get; set; }
    }
}

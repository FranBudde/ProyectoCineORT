using System.ComponentModel.DataAnnotations;

namespace ProyectoCine.Models
{
    public class Usuario
    {

        [Key]
        public int IdUser { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(3)]
        public string Name { get; set; }

        [Required, MinLength(3)]
        public string Password { get; set; }
    }
}

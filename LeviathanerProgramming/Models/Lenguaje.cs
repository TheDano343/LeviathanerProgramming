using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeviathanerProgramming.Models
{
    public class Lenguaje
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdLenguaje { get; set; }

        [Required(ErrorMessage = "El campo de nombre Lenguaje es obligatorio")]
        public string? NombreLenguaje { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Programming_Courses.Models
{
    public class NivelDificultad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdNivelDificultad { get; set; }

        [Required(ErrorMessage = "El campo de nombre dificultad es obligatorio")]
        public string? NombreDificultad { get; set; }
    }
}

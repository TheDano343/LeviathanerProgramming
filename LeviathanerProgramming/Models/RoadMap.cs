using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeviathanerProgramming.Models
{
    public class RoadMap
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdRoadMap { get; set; }

        [Required(ErrorMessage = "El campo de titulo es obligatorio")]
        public string? Titulo { get; set; }

        [Required(ErrorMessage = "El campo de descripcion es obligatorio")]
        [Column(TypeName = "longtext")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "Debes seleccionar un Lenguaje de Programacion")]
        public int? Lenguaje_Programacion_Id { get; set; }
        [ForeignKey("Lenguaje_Programacion_Id")]
        public Lenguaje? ListaLenguaje { get; set; }
    }
}

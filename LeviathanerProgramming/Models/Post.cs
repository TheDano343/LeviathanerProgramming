using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeviathanerProgramming.Models
{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPost { get; set; }

        [Required(ErrorMessage = "El campo de titulo es obligatorio")]
        public string? Titulo { get; set; }

        [Required(ErrorMessage = "El campo de descripcion es obligatorio")]
        [Column(TypeName = "longtext")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "Debes seleccionar un nivel Dificultad")]
        public int? Nivel_Dificiltad_Id { get; set; }
        [ForeignKey("Nivel_Dificiltad_Id")]
        public NivelDificultad? ListaDificultad { get; set; }

        [Required(ErrorMessage = "Debes seleccionar un RoadMap")]
        public int? Road_Map_Id { get; set; }
        [ForeignKey("Road_Map_Id")]
        public RoadMap? ListaRoadMap { get; set; }


    }
}

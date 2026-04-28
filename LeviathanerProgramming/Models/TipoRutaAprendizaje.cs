using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Programming_Courses.Models
{
    public class TipoRutaAprendizaje
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdRutaAprendizaje { get; set; }

        [Required(ErrorMessage = "El campo de nombre tipo aprendizaje es obligatorio")]
        public string? NombreTipoAprenidzaje { get; set; }

        [Required(ErrorMessage = "Debes seleccionar un RoadMap")]
        public int? Road_Map_Id { get; set; }
        [ForeignKey("Road_Map_Id")]
        public RoadMap? ListaRoadMap { get; set; }
    }
}

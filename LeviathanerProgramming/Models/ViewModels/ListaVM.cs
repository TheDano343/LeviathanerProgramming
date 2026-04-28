using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using LeviathanerProgramming.Models;

namespace LeviathanerProgramming.Models.ViewModels
{
    public class ListaVM
    {
        public Lenguaje? oLenguaje { get; set; }
        public List<SelectListItem>? oListaLenguaje { get; set; }
        public Post? oPost { get; set; }
        public List<SelectListItem>? oListaPost { get; set; }
        public RoadMap? oRoadMap { get; set; }
        public List<SelectListItem>? oListaRoadMap { get; set; }
        public NivelDificultad? oNivelDificultad { get; set; }
        public List<SelectListItem>? oListaNivelDificultad { get; set; }
        public TipoRutaAprendizaje? oTipoRutaAprendizaje { get; set; }
        public List<SelectListItem>? oListaTipoRutaAprendizaje { get; set; }

    }
}

using LeviathanerProgramming.Models;
using LeviathanerProgramming.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LeviathanerProgramming.Controllers
{
     public class TipoRutaAprendizajeController : Controller
     {
        private readonly ApplicationDbContext _applicationDbContext;

        public TipoRutaAprendizajeController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        [Authorize]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Lista()
        {
            List<TipoRutaAprendizaje> lista = _applicationDbContext.TipoRutaAprendizajes.Include(c => c.ListaRoadMap).ToList();
            return View(lista);
        }

        [Authorize]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Nuevo(int IdRutaAprendizaje)
        {
            ListaVM oListaVM = new ListaVM()
            {
                oTipoRutaAprendizaje = new TipoRutaAprendizaje(),
                oListaRoadMap = _applicationDbContext.RoadMaps.Select(roadmap => new SelectListItem()
                {
                    Text = roadmap.Titulo,
                    Value = roadmap.IdRoadMap.ToString()
                }).ToList()
            };

            if (IdRutaAprendizaje != 0)
            {
                oListaVM.oRoadMap = _applicationDbContext.RoadMaps.Find(IdRutaAprendizaje);
            }

            return View(oListaVM);
        }

        [Authorize]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Nuevo(ListaVM oListaVM)
        {

            if (!ModelState.IsValid)
            {
                oListaVM.oListaRoadMap = _applicationDbContext.RoadMaps.Select(roadmap => new SelectListItem()
                {
                    Text = roadmap.Titulo,
                    Value = roadmap.IdRoadMap.ToString()
                }).ToList();

                return View(oListaVM);
            }

            _applicationDbContext.TipoRutaAprendizajes.Add(oListaVM.oTipoRutaAprendizaje);
            _applicationDbContext.SaveChanges();

            TempData["Exito"] = "Tipo Ruta Aprendizaje Creado Correctamente";
            return RedirectToAction("Lista", "TipoRutaAprendizaje");
        }

        [Authorize]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Editar(int IdRutaAprendizaje)
        {
            ListaVM oListaVM = new ListaVM()
            {
                oTipoRutaAprendizaje = new TipoRutaAprendizaje(),

                oListaRoadMap = _applicationDbContext.RoadMaps.Select(roadmap => new SelectListItem()
                {
                    Text = roadmap.Titulo,
                    Value = roadmap.IdRoadMap.ToString()
                }).ToList()
            };

            if (IdRutaAprendizaje != 0)
            {
                oListaVM.oTipoRutaAprendizaje = _applicationDbContext.TipoRutaAprendizajes.Find(IdRutaAprendizaje);
            }

            return View(oListaVM);
        }

        [Authorize]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Editar(ListaVM oListaVM)
        {
            if (!ModelState.IsValid)
            {
                oListaVM.oListaRoadMap = _applicationDbContext.RoadMaps.Select(roadmap => new SelectListItem()
                {
                    Text = roadmap.Titulo,
                    Value = roadmap.IdRoadMap.ToString()
                }).ToList();

                return View(oListaVM);
            }

            _applicationDbContext.TipoRutaAprendizajes.Update(oListaVM.oTipoRutaAprendizaje);
            _applicationDbContext.SaveChanges();

            TempData["Actualizado"] = "Tipo Ruta Aprendizaje Actualizado Correctamente";
            return RedirectToAction("Lista", "TipoRutaAprendizaje");
        }

        [Authorize]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Eliminar(int IdRutaAprendizaje)
        {
            TipoRutaAprendizaje tiporutaaprendizaje = await _applicationDbContext.TipoRutaAprendizajes.FirstAsync(e => e.IdRutaAprendizaje == IdRutaAprendizaje);

            _applicationDbContext.TipoRutaAprendizajes.Remove(tiporutaaprendizaje);
            TempData["Eliminado"] = "RoadMap Eliminado Correctamente";
            await _applicationDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }
    }
}

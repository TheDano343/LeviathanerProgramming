using LeviathanerProgramming.Models;
using LeviathanerProgramming.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LeviathanerProgramming.Controllers
{
    public class RoadMapController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public RoadMapController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [Authorize]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Lista()
        {
            List<RoadMap> lista = _applicationDbContext.RoadMaps.Include(c => c.ListaLenguaje).ToList();
            return View(lista);
        }

        [Authorize]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Nuevo(int IdLenguaje)
        {
            ListaVM oListaVM = new ListaVM()
            {
                oRoadMap = new RoadMap(),
                oListaLenguaje = _applicationDbContext.Lenguajes.Select(lenguaje => new SelectListItem()
                {
                    Text = lenguaje.NombreLenguaje,
                    Value = lenguaje.IdLenguaje.ToString()
                }).ToList()
            };

            if (IdLenguaje != 0)
            {
                oListaVM.oLenguaje = _applicationDbContext.Lenguajes.Find(IdLenguaje);
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
                // RECARGAR LA LISTA AQUÍ: Si no, el select llega vacío a la vista
                oListaVM.oListaLenguaje = _applicationDbContext.Lenguajes.Select(lenguaje => new SelectListItem()
                {
                    Text = lenguaje.NombreLenguaje,
                    Value = lenguaje.IdLenguaje.ToString()
                }).ToList();

                return View(oListaVM);
            }

            // Si es válido, guardamos
            _applicationDbContext.RoadMaps.Add(oListaVM.oRoadMap);
            _applicationDbContext.SaveChanges();

            TempData["Exito"] = "RoadMap Creado Correctamente";
            return RedirectToAction("Lista", "RoadMap");
        }

        [Authorize]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Editar(int IdRoadMap)
        {
            ListaVM oListaVM = new ListaVM()
            {
                oRoadMap = new RoadMap(),

                oListaLenguaje = _applicationDbContext.Lenguajes.Select(lenguaje => new SelectListItem()
                {
                    Text = lenguaje.NombreLenguaje,
                    Value = lenguaje.IdLenguaje.ToString()
                }).ToList()
            };

            if (IdRoadMap != 0)
            {
                oListaVM.oRoadMap = _applicationDbContext.RoadMaps.Find(IdRoadMap);
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
                // RECARGAR LA LISTA AQUÍ
                oListaVM.oListaLenguaje = _applicationDbContext.Lenguajes.Select(lenguaje => new SelectListItem()
                {
                    Text = lenguaje.NombreLenguaje,
                    Value = lenguaje.IdLenguaje.ToString()
                }).ToList();

                return View(oListaVM);
            }

            _applicationDbContext.RoadMaps.Update(oListaVM.oRoadMap);
            _applicationDbContext.SaveChanges();

            TempData["Actualizado"] = "RoadMap Actualizado Correctamente";
            return RedirectToAction("Lista", "RoadMap");
        }

        [Authorize]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Eliminar(int IdRoadMap)
        {
            RoadMap roadmap = await _applicationDbContext.RoadMaps.FirstAsync(e => e.IdRoadMap == IdRoadMap);

            _applicationDbContext.RoadMaps.Remove(roadmap);
            TempData["Eliminado"] = "RoadMap Eliminado Correctamente";
            await _applicationDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }
    }
}

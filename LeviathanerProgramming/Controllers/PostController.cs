using LeviathanerProgramming.Models;
using LeviathanerProgramming.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace LeviathanerProgramming.Controllers
{
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PostController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [Authorize]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Lista()
        {
            List<Post> lista = _applicationDbContext.Posts.Include(c => c.ListaDificultad).Include(c => c.ListaRoadMap).ToList();
            return View(lista);
        }

        [Authorize]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Nuevo()
        {
            ListaVM oListaVM = new ListaVM()
            {
                oPost = new Post(),

                oListaNivelDificultad = _applicationDbContext.NivelDificultades.Select(nivel => new SelectListItem()
                {
                    Text = nivel.NombreDificultad,
                    Value = nivel.IdNivelDificultad.ToString()
                }).ToList(),

                oListaRoadMap = _applicationDbContext.RoadMaps.Select(roadmap => new SelectListItem()
                {
                    Text = roadmap.Titulo,
                    Value = roadmap.IdRoadMap.ToString()
                }).ToList(),

            };

            return View(oListaVM);
        }

        [Authorize]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Nuevo(ListaVM oListaVM)
        {
            if (!ModelState.IsValid)
            {
                oListaVM.oListaNivelDificultad = _applicationDbContext.NivelDificultades.Select(nivel => new SelectListItem()
                {
                    Text = nivel.NombreDificultad,
                    Value = nivel.IdNivelDificultad.ToString()
                }).ToList();

                oListaVM.oListaRoadMap = _applicationDbContext.RoadMaps.Select(roadmap => new SelectListItem()
                {
                    Text = roadmap.Titulo,
                    Value = roadmap.IdRoadMap.ToString()
                }).ToList();

                return View(oListaVM);
            }
            _applicationDbContext.Posts.Add(oListaVM.oPost);
            _applicationDbContext.SaveChanges();

            TempData["Exito"] = "Post Creado Correctamente";
            return RedirectToAction("Lista", "Post");
        }

        [Authorize]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Editar(int IdPost)
        {
            ListaVM oListaVM = new ListaVM()
            {
                oPost = new Post(),

                oListaNivelDificultad = _applicationDbContext.NivelDificultades.Select(nivel => new SelectListItem()
                {
                    Text = nivel.NombreDificultad,
                    Value = nivel.IdNivelDificultad.ToString()
                }).ToList(),

                oListaRoadMap = _applicationDbContext.RoadMaps.Select(roadmap => new SelectListItem()
                {
                    Text = roadmap.Titulo,
                    Value = roadmap.IdRoadMap.ToString()
                }).ToList(),

            };

            if (IdPost != 0)
            {
                oListaVM.oPost = _applicationDbContext.Posts.Find(IdPost);
            }
            return View(oListaVM);
        }

        [Authorize]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Editar(ListaVM oListaVM)
        {
            if (!ModelState.IsValid)
            {

                oListaVM.oListaNivelDificultad = _applicationDbContext.NivelDificultades.Select(nivel => new SelectListItem()
                {
                    Text = nivel.NombreDificultad,
                    Value = nivel.IdNivelDificultad.ToString()
                }).ToList();

                oListaVM.oListaRoadMap = _applicationDbContext.RoadMaps.Select(roadmap => new SelectListItem()
                {
                    Text = roadmap.Titulo,
                    Value = roadmap.IdRoadMap.ToString()
                }).ToList();

                return View(oListaVM);

            }

            _applicationDbContext.Posts.Update(oListaVM.oPost);
            _applicationDbContext.SaveChanges();

            TempData["Actualizado"] = "Post Actualizado Correctamente";
            return RedirectToAction("Lista", "Post");
        }

        [Authorize]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Eliminar(int IdPost)
        {
            Post post = await _applicationDbContext.Posts.FirstAsync(e => e.IdPost == IdPost);

            _applicationDbContext.Posts.Remove(post);
            TempData["Eliminado"] = "Post Eliminado Correctamente";
            await _applicationDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }
    }
}

using LeviathanerProgramming.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LeviathanerProgramming.Models;

namespace LeviathanerProgramming.Controllers
{
    public class NivelDificultadController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public NivelDificultadController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [Authorize]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<NivelDificultad> lista = await _applicationDbContext.NivelDificultades.ToListAsync();
            return View(lista);
        }

        [Authorize]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Nuevo()
        {
            return View();
        }

        [Authorize]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Nuevo(NivelDificultad niveldificultad)
        {
            if (!ModelState.IsValid)
            {
                return View(niveldificultad);
            }

            await _applicationDbContext.NivelDificultades.AddAsync(niveldificultad);
            await _applicationDbContext.SaveChangesAsync();
            TempData["Exito"] = "Nivel Dificultad Creada Correctamente";
            return RedirectToAction(nameof(Lista));
        }

        [Authorize]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Editar(int IdNivelDificultad)
        {
            NivelDificultad niveldificultad = await _applicationDbContext.NivelDificultades.FirstAsync(e => e.IdNivelDificultad == IdNivelDificultad);

            return View(niveldificultad);
        }

        [Authorize]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Editar(NivelDificultad niveldificultad)
        {
            if (!ModelState.IsValid)
            {
                return View(niveldificultad);
            }

            _applicationDbContext.NivelDificultades.Update(niveldificultad);
            await _applicationDbContext.SaveChangesAsync();
            TempData["Actualizado"] = "Nivel Dificultad Actualizada Correctamente";
            return RedirectToAction(nameof(Lista));
        }

        [Authorize]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Eliminar(int IdNivelDificultad)
        {
            NivelDificultad niveldificultad = await _applicationDbContext.NivelDificultades.FirstAsync(e => e.IdNivelDificultad == IdNivelDificultad);

            _applicationDbContext.NivelDificultades.Remove(niveldificultad);
            TempData["Eliminado"] = "Nivel Dificultad Eliminada Correctamente";
            await _applicationDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }
    }
}

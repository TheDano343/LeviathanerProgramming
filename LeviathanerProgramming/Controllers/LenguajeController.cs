using LeviathanerProgramming.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeviathanerProgramming.Controllers
{
    public class LenguajeController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public LenguajeController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        [Authorize]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<Lenguaje> lista = await _applicationDbContext.Lenguajes.ToListAsync();
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
        public async Task<IActionResult> Nuevo(Lenguaje lenguaje)
        {
            if (!ModelState.IsValid)
            {
                return View(lenguaje);
            }

            await _applicationDbContext.Lenguajes.AddAsync(lenguaje);
            await _applicationDbContext.SaveChangesAsync();
            TempData["Exito"] = "Lenguaje de Programación Agregado Correctamente";
            return RedirectToAction(nameof(Lista));
        }

        [Authorize]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Editar(int IdLenguaje)
        {
            Lenguaje lenguaje = await _applicationDbContext.Lenguajes.FirstAsync(e => e.IdLenguaje == IdLenguaje);
            return View(lenguaje);
        }

        [Authorize]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Editar(Lenguaje lenguaje)
        {
            if (!ModelState.IsValid)
            {
                return View(lenguaje);
            }

            _applicationDbContext.Lenguajes.Update(lenguaje);
            await _applicationDbContext.SaveChangesAsync();
            TempData["Actualizado"] = "Lenguaje de Programación Actualizado Correctamente";
            return RedirectToAction(nameof(Lista));
        }

        [Authorize]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Eliminar(int IdLenguaje)
        {
            Lenguaje lenguaje = await _applicationDbContext.Lenguajes.FirstAsync(e => e.IdLenguaje == IdLenguaje);

            _applicationDbContext.Lenguajes.Remove(lenguaje);
            TempData["Eliminado"] = "Lenguaje de Programación Eliminado Correctamente";
            await _applicationDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }
    }
}

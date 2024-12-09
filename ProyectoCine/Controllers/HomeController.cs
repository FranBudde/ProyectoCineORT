using Microsoft.AspNetCore.Mvc;
using ProyectoCine.Models;
using System.Diagnostics;

namespace ProyectoCine.Controllers
{
    public class HomeController : Controller
    {
        private readonly CineContext _context;

        public HomeController(CineContext context)
        {
            _context = context;
        }

        // GET: /Home
        public IActionResult Index()
        {
            var movies = _context.Peliculas
                .Select(p => new Pelicula
                {
                    IdPelicula = p.IdPelicula,
                    NamePelicula = p.NamePelicula,
                    Duracion = p.Duracion,
                    Genero = p.Genero
                }).ToList();

            return View(movies);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

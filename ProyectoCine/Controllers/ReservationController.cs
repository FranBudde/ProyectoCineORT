using Microsoft.AspNetCore.Mvc;
using ProyectoCine.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace ProyectoCine.Controllers
{
    [Route("reservacion")]
    public class ReservationController : Controller
    {
        private readonly CineContext _context;

        public ReservationController(CineContext context)
        {
            _context = context;
        }

        // GET: /reservacion
        [HttpGet("")]
        public IActionResult Index()
        {
            if (!HttpContext.Session.GetInt32("UserId").HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.Movies = _context.Peliculas.ToList();
            ViewBag.Schedules = _context.Horarios.ToList();

            return View();
        }

        // POST: /reservacion/seleccionar-butaca
        [HttpPost("seleccionar-butaca")]
        public IActionResult SeleccionarButaca(int selectedMovieId, int selectedScheduleId)
        {
            if (!HttpContext.Session.GetInt32("UserId").HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            // Verificar existencia de la película seleccionada
            var movie = _context.Peliculas.Include(p => p.Sala)
                .FirstOrDefault(p => p.IdPelicula == selectedMovieId);

            if (movie == null)
            {
                return NotFound("Película no encontrada.");
            }

            // Verificar el horario seleccionado
            var schedule = _context.PeliculaHorarios
                .Include(ph => ph.Horario)
                .FirstOrDefault(ph => ph.IdPeliculaHorario == selectedScheduleId);

            if (schedule == null)
            {
                return NotFound("Horario no encontrado.");
            }

            // Obtener las butacas reservadas
            var reservedSeats = _context.Reservas
                .Where(r => r.IdPeliculaHorario == selectedScheduleId)
                .Select(r => r.IdButaca)
                .ToList();

            // Filtrar butacas disponibles
            var availableSeats = _context.Butacas
                .Where(b => !reservedSeats.Contains(b.IdButaca) && b.IdSala == movie.IdSala)
                .OrderBy(b => b.Letra)
                .ThenBy(b => b.numeroButaca)
                .ToList();

            // Pasar datos a la vista
            ViewBag.Movie = movie;
            ViewBag.Schedule = schedule;
            ViewBag.Seats = availableSeats;

            return View("Seats");
        }

        // POST: /reservacion/finalizar-compra
        [HttpPost("finalizar-compra")]
        public IActionResult FinalizarCompra(int selectedSeatId, int selectedScheduleId, string paymentMethod)
        {
            if (!HttpContext.Session.GetInt32("UserId").HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            var userId = HttpContext.Session.GetInt32("UserId").Value;

            // Verificar si la butaca ya está reservada
            var existingReservation = _context.Reservas
                .Any(r => r.IdPeliculaHorario == selectedScheduleId && r.IdButaca == selectedSeatId);

            if (existingReservation)
            {
                return BadRequest("La butaca seleccionada ya está reservada.");
            }

            // Crear nueva reserva
            var newReservation = new Reserva
            {
                IdUsuario = userId,
                IdPeliculaHorario = selectedScheduleId,
                IdButaca = selectedSeatId
            };

            _context.Reservas.Add(newReservation);
            _context.SaveChanges();

            // Obtener detalles para mostrar en la vista
            var seat = _context.Butacas.Include(b => b.Sala).FirstOrDefault(b => b.IdButaca == selectedSeatId);
            var movie = _context.Peliculas.FirstOrDefault(p => p.IdSala == seat.IdSala);

            ViewBag.MovieName = movie?.NamePelicula;
            ViewBag.SeatRow = seat?.Letra;
            ViewBag.SeatNumber = seat?.numeroButaca;
            ViewBag.SalaId = seat?.IdSala;
            ViewBag.PaymentMethod = paymentMethod;

            return View("PurchaseComplete");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using ProyectoCine.Models;
using System.Linq;
using System.Net.Mail;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ProyectoCine.Controllers
{
    [Route("reservacion")]
    public class ReservationController : Controller
    {
        private readonly CineContext _context;
        private readonly IConfiguration _configuration;

        public ReservationController(IConfiguration configuration, CineContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        // GET: /reservacion
        [HttpGet("")]
        public IActionResult Index()
        {
            // Verificar que el usuario esté logueado
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
                return RedirectToAction("IniciarSesion", "Cuenta");
            }

            // Cargar película seleccionada
            var movie = _context.Peliculas.FirstOrDefault(m => m.IdPelicula == selectedMovieId);

            // Cargar PeliculaHorario con la relación Horario
            var schedule = _context.PeliculaHorarios
                .Include(ph => ph.Horario)
                .FirstOrDefault(ph => ph.IdPeliculaHorario == selectedScheduleId);

            if (movie == null || schedule == null)
            {
                return NotFound("La película o el horario seleccionados no existen.");
            }

            // Obtener las butacas reservadas para el horario seleccionado
            var reservedSeats = _context.Reservas
                .Where(r => r.IdPeliculaHorario == selectedScheduleId)
                .Select(r => r.IdButaca)
                .ToList();

            // Filtrar butacas disponibles
            var seats = _context.Butacas
                .Where(b => !reservedSeats.Contains(b.IdButaca) && b.IdSala == movie.IdSala)
                .OrderBy(b => b.Letra)
                .ThenBy(b => b.numeroButaca)
                .ToList();

            // Pasar datos a la vista
            ViewBag.Movie = movie;
            ViewBag.Schedule = schedule;
            ViewBag.Seats = seats;

            return View("Seats");
        }




        // POST: /reservacion/finalizar-compra
        [HttpPost("finalizar-compra")]
        public IActionResult FinalizarCompra(int selectedSeatId, int selectedScheduleId, string paymentMethod)
        {
            if (!HttpContext.Session.GetInt32("UserId").HasValue)
            {
                return RedirectToAction("IniciarSesion", "Cuenta");
            }

            var userId = HttpContext.Session.GetInt32("UserId").Value;

            // Verificar si la butaca ya está reservada para el mismo horario
            var existingReservation = _context.Reservas
                .Any(r => r.IdPeliculaHorario == selectedScheduleId && r.IdButaca == selectedSeatId);

            if (existingReservation)
            {
                return BadRequest("La butaca seleccionada ya está reservada para este horario.");
            }

            // Crear una nueva reserva
            var newReservation = new Reserva
            {
                IdUsuario = userId,
                IdPeliculaHorario = selectedScheduleId,
                IdButaca = selectedSeatId
            };

            _context.Reservas.Add(newReservation);
            _context.SaveChanges();

            var seat = _context.Butacas.FirstOrDefault(b => b.IdButaca == selectedSeatId);
            var movie = _context.Peliculas.FirstOrDefault(p => p.IdPelicula == seat.Sala.IdSala);

            ViewBag.MovieName = movie.NamePelicula;
            ViewBag.SeatRow = seat.Letra;
            ViewBag.SeatNumber = seat.numeroButaca;
            ViewBag.SalaId = seat.Sala.IdSala;
            ViewBag.PaymentMethod = paymentMethod;

            return View("PurchaseComplete");
        }



    }
}

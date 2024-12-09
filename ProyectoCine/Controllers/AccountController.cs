using Microsoft.AspNetCore.Mvc;
using ProyectoCine.Models;
using Microsoft.AspNetCore.Http;

namespace ProyectoCine.Controllers
{
    [Route("cuenta")]
    public class AccountController : Controller
    {
        private readonly CineContext _context;

        public AccountController(CineContext context)
        {
            _context = context;
        }

        // GET: /cuenta/registro
        [HttpGet("registro")]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /cuenta/registro
        [HttpPost("registro")]
        public IActionResult Register(Usuario user)
        {
            if (ModelState.IsValid)
            {
                _context.Usuarios.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(user);
        }

        // GET: /cuenta/iniciar-sesion
        [HttpGet("iniciar-sesion")]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /cuenta/iniciar-sesion
        [HttpPost("iniciar-sesion")]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Usuarios.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user != null)
            {
                HttpContext.Session.SetInt32("UserId", user.IdUser); // Guarda el ID del usuario en la sesión
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ErrorMessage = "Credenciales inválidas.";
            return View();
        }

        // GET: /cuenta/cerrar-sesion
        [HttpGet("cerrar-sesion")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Limpia la sesión
            return RedirectToAction("Login");
        }
    }
}

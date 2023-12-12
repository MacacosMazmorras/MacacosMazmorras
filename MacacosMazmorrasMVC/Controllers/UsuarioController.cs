using MacacosMazmorrasMVC.Models;
using MacacosMazmorrasMVC.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace MacacosMazmorrasMVC.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioDAL usuarioDAL;

        public UsuarioController()
        {
            usuarioDAL = new UsuarioDAL(Conexion.StringBBDD);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Home()
        {
            var sessionId = HttpContext.Session.GetInt32("_UsuarioId"); //do a get from session
            ViewBag.SessionId = sessionId; //pass de variable to a view
            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignIn(Usuario newUsuario)
        {
            if (ModelState.IsValid)
            {
                usuarioDAL.InsertUsuario(newUsuario);
                return RedirectToAction("Index", "Usuario"); //reditect to log in
            }

            return View(newUsuario);
        }
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LogIn([Bind("UsuarioMail,UsuarioPassword")] Usuario user)
        {

            Usuario sessionUser = usuarioDAL.GetUser(user);

            if (sessionUser != null)
            {
                HttpContext.Session.SetInt32("_UsuarioId", sessionUser.UsuarioId); //create a session variable
                return RedirectToAction("Home", "Usuario"); //redirect to home
            }
            else
                return View();
        }
    }
}

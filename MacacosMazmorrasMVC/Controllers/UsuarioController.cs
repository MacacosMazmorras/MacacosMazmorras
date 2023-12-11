using MacacosMazmorrasMVC.Models;
using MacacosMazmorrasMVC.DAL;
using Microsoft.AspNetCore.Mvc;

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

            return RedirectToAction("Index", "Usuario");
        }
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult LogIn([Bind("UsuarioMail,UsuarioPassword")] Usuario user)
        {

            bool userExist = usuarioDAL.CheckUser(user);
            if (userExist)
                return RedirectToAction("Index", "Home"); //redirect to home
            else
                return View();
        }
    }
}

using MacacosMazmorrasMVC.DAL;
using Microsoft.AspNetCore.Mvc;

namespace MacacosMazmorrasMVC.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioDAL usuarioDAL;

        public UsuarioController()
        {
            usuarioDAL = new UsuarioDAL(Conexion.CadenaBBDD);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}

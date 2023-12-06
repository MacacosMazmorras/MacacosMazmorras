using Microsoft.AspNetCore.Mvc;

namespace MacacosMazmorrasMVC.Controllers
{
    public class GlossaryController : Controller
    {
        public IActionResult Monsters()
        {
            return View();
        }

        public IActionResult Spells() 
        {
            return View();
        }
    }
}

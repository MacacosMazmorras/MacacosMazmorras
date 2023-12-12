using MacacosMazmorrasMVC.DAL;
using MacacosMazmorrasMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace MacacosMazmorrasMVC.Controllers
{
    [Authorize]
    public class SheetCustomController : Controller
    {
        private readonly SheetCustomDAL sheetCustomDal;
        public SheetCustomController()
        {
            sheetCustomDal = new SheetCustomDAL(Conexion.StringBBDD);
        }

        public IActionResult Index()
        {
            //Recovers the id from the user logged in
            //newCharacter.FKUsuarioId = HttpContext.Session.GetInt32("_UsuarioId");

            //Change it to show only user custom sheets
            List<SheetCustom> lstSheetCustom = sheetCustomDal.ObtainAllSheets();
            return View(lstSheetCustom);
        }

        public IActionResult NewCharacterForm()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NewCharacterForm(SheetCustom newCharacter)
        {
            if (ModelState.IsValid)
            {
                sheetCustomDal.InsertSheet(newCharacter);
                return RedirectToAction("Index", "SheetCustom"); //redirect to SheetCustomList
            }

            return View(newCharacter);
        }
    }
}

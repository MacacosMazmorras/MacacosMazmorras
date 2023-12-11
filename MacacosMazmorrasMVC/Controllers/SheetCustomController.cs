using MacacosMazmorrasMVC.DAL;
using MacacosMazmorrasMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace MacacosMazmorrasMVC.Controllers
{
    public class SheetCustomController : Controller
    {
        private readonly SheetCustomDAL sheetCustomDal;
        public SheetCustomController()
        {
            sheetCustomDal = new SheetCustomDAL(Conexion.StringBBDD);
        }

        public IActionResult Index(SheetCustom sheetCustom)
        {
            List<SheetCustom> lstSheetCustom = sheetCustomDal.ObtainUserSheets(1);
            return View(lstSheetCustom);
        }

    }
}

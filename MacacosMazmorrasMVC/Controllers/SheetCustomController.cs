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

        public IActionResult Index()
        {
            List<SheetCustom> lstSheetCustom = sheetCustomDal.ObtainAllSheets();
            return View(lstSheetCustom);
        }

    }
}

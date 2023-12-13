using MacacosMazmorrasMVC.DAL;
using MacacosMazmorrasMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MacacosMazmorrasMVC.Controllers
{
    [Authorize]
    public class SheetCustomController : Controller
    {
        private readonly SheetCustomDAL sheetCustomDal;
        private readonly CampaignDAL campaignDal;
        public SheetCustomController()
        {
            sheetCustomDal = new SheetCustomDAL(Conexion.StringBBDD);
            campaignDal = new CampaignDAL(Conexion.StringBBDD);
        }

        [HttpGet]
        public IActionResult Index()
        {
            //Recovers the id from the user logged in
            int userId = HttpContext.Session.GetInt32("_UsuarioId") ?? 0;

            //Change it to show only user custom sheets
            List<SheetCustom> lstSheetCustom = sheetCustomDal.ObtainUserSheets(userId);
            return View(lstSheetCustom);
        }

        public IActionResult NewCharacterForm()
        {
            //Recovers the id from the user logged in
            int userId = HttpContext.Session.GetInt32("_UsuarioId") ?? 0;

            //Get all the campaigns from the user and store each campaign name
            List <Campaign> userCampaigns = campaignDal.ObtainAllUserCampaigns(userId);

            //Creamos una lista para el dropdown especificando el texto mostrado y el valor real de la selección,
            //en este caso queremos quedarnos con el ID de campaña
            ViewBag.Campaigns = userCampaigns.Select(campaign => 
                                                     new SelectListItem 
                                                     { 
                                                         Text = campaign.CampaignName, 
                                                         Value = campaign.CampaignId.ToString() 
                                                     }).ToList();
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

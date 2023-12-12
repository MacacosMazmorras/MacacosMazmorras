using MacacosMazmorrasMVC.DAL;
using MacacosMazmorrasMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace MacacosMazmorrasMVC.Controllers
{
    public class CampaignController : Controller
    {
        private readonly CampaignDAL campaignDAL;

        public CampaignController()
        {
            campaignDAL = new CampaignDAL(Conexion.StringBBDD);
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NewCampaignForm()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult NewCampaignForm(Campaign newCampaign)
        {
            //Recovers the id from the user logged in
            newCampaign.FKUsuarioId = HttpContext.Session.GetInt32("_UsuarioId");

            if (ModelState.IsValid)
            {
                campaignDAL.InsertCampaign(newCampaign);
                return RedirectToAction("Index", "Campaign"); //redirect to Campaign
            }

            return View(newCampaign);
        }

        public IActionResult UpdateCampaignForm()
        {
            Campaign modifyCampaign = campaignDAL.ObtainUserCampaign(1);

            ViewBag.CampaignName = modifyCampaign.CampaignName;
            ViewBag.CampaignDesc = modifyCampaign.CampaignDesc;
            ViewBag.CampaignMap = modifyCampaign.CampaignMap;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateCampaignForm(Campaign updateCampaign)
        {
            if (ModelState.IsValid)
            {
                campaignDAL.UpdateCampaign(updateCampaign);
                return RedirectToAction("Index", "Campaign"); //redirect to Campaign
            }

            return View(updateCampaign);
        }

    }
}

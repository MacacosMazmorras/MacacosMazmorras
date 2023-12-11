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
            //
            //recuperar id del usuario para la FK
            //
            //

            if (ModelState.IsValid)
            {
                campaignDAL.InsertCampaign(newCampaign);
                return RedirectToAction("Index", "Campaign"); //redirect to Campaign
            }

            return View(newCampaign);
        }

        public IActionResult UpdateCampaignForm()
        {
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

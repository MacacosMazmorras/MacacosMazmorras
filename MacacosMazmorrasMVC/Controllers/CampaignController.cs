using MacacosMazmorrasMVC.DAL;
using MacacosMazmorrasMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


namespace MacacosMazmorrasMVC.Controllers
{
    [Authorize]
    public class CampaignController : Controller
    {
        private readonly CampaignDAL campaignDAL;

        public CampaignController()
        {
            campaignDAL = new CampaignDAL(Conexion.StringBBDD);
        }

        public IActionResult Index()
        {
            int userId = HttpContext.Session.GetInt32("_UsuarioId") ?? 0;

            // RECUPERAR EL USUARIOOOO!!
            //
            List<Campaign> lstCampaign = campaignDAL.ObtainAllUserCampaigns(100);
            //
            //

            return View(lstCampaign);
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
                ViewBag.CreateCampaignSuccess = true;
                return RedirectToAction("Index", "Campaign"); //redirect to Campaign
            }

            return View(newCampaign);
        }

        public IActionResult UpdateCampaignForm(int campaignId)
        {
            // Retrieve the campaign using the campaign ID
            Campaign modifyCampaign = campaignDAL.ObtainUserCampaign(campaignId);

            return View(modifyCampaign);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateCampaignForm(Campaign updateCampaign)
        {
            if (ModelState.IsValid)
            {
                campaignDAL.UpdateCampaign(updateCampaign);
                ViewBag.UpdateCampaignSuccess = true;
                return RedirectToAction("Index", "Campaign"); //redirect to Campaign
            }

            return View(updateCampaign);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCampaign(int campaignId)
        {
            // Delete the campaign from the database based on the ID
            campaignDAL.DeleteCampaign(campaignId);

            // Redirect to a success page or any other appropriate action
            return RedirectToAction("Index", "Campaign");
        }
    }
}

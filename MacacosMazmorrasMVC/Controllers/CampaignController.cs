using MacacosMazmorrasMVC.DAL;
using MacacosMazmorrasMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;


namespace MacacosMazmorrasMVC.Controllers
{
    [Authorize]
    public class CampaignController : Controller
    {
        private readonly CampaignDAL campaignDAL;
        private readonly SesionDAL sesionDAL;
        private readonly SheetCustomDAL sheetCustomDAL;

        public CampaignController()
        {
            campaignDAL = new CampaignDAL(Conexion.StringBBDD);
            sesionDAL = new SesionDAL(Conexion.StringBBDD);
            sheetCustomDAL = new SheetCustomDAL(Conexion.StringBBDD);
        }

        public IActionResult Index()
        {
            int usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            // RECUPERAR EL USUARIOOOO!!
            //
            List<Campaign> lstCampaign = campaignDAL.ObtainAllUserCampaigns(usuarioId);
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
            newCampaign.FKUsuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

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
            if (Request.Form["confirmed"] == "true")
            {
                campaignDAL.DeleteCampaign(campaignId);

                return RedirectToAction("Index", "Campaign");
            }

            return NoContent();
        }

        public IActionResult CamapignWithSesion(int campaignId)
        {

            return RedirectToAction("Index", "Sesion");
        }
    }
}

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

        private readonly ImageBBController imageBBController;

        public CampaignController(ImageBBDAL imageBBDAL)
        {
            campaignDAL = new CampaignDAL(Conexion.StringBBDD);
            sesionDAL = new SesionDAL(Conexion.StringBBDD);
            sheetCustomDAL = new SheetCustomDAL(Conexion.StringBBDD);

            imageBBController = new ImageBBController(imageBBDAL);
        }

        public IActionResult Index()
        {
            int usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            // recovery user!!
            //
            List<Campaign> lstCampaign = campaignDAL.ObtainAllUserCampaigns(usuarioId);
            //
            //

            return View(lstCampaign);
        }
        public IActionResult RedirectToCampaign(int campaignId)
        {
            HttpContext.Session.SetInt32("_selectedCampaignId", campaignId);

            return RedirectToAction("CampaignSesions", "Campaign");
        }
        public IActionResult CampaignSesions()
        {
            int campaignId = HttpContext.Session.GetInt32("_selectedCampaignId") ?? 0;

            List<Sesion> lstSesion = sesionDAL.ObtainAllCampaignSesions(campaignId);

            ViewBag.SelectedCampaign = campaignDAL.ObtainCampaign(campaignId);

            return View(lstSesion);
        }

        public IActionResult RedirectToSesion(int sessionId)
        {
            HttpContext.Session.SetInt32("_selectedSessionId", sessionId);

            return RedirectToAction("Index", "Session");
        }

        public IActionResult NewCampaignForm()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewCampaignForm(Campaign newCampaign, IFormFile campaignMapFile)
        {
            //Recovers the id from the user logged in
            newCampaign.FKUsuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            //// File validation and process for url image
            if (campaignMapFile != null && campaignMapFile.Length > 0)
            {
                string url = await imageBBController.UploadImageAsync(campaignMapFile);
                newCampaign.CampaignMap = url;
            }
            else
                newCampaign.CampaignMap = "https://i.ibb.co/frQkKbr/World.png";

            //inject the info
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
            Campaign modifyCampaign = campaignDAL.ObtainCampaign(campaignId);

            return View(modifyCampaign);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCampaignForm(Campaign updateCampaign, IFormFile campaignMapFile)
        {
            //// File validation and process for url image
            if (campaignMapFile != null && campaignMapFile.Length > 0)
            {
                string url = await imageBBController.UploadImageAsync(campaignMapFile);
                updateCampaign.CampaignMap = url;
            }
            else
                updateCampaign.CampaignMap = "https://i.ibb.co/frQkKbr/World.png";

            //inject form
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

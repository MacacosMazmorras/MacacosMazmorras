using MacacosMazmorrasMVC.DAL;
using MacacosMazmorrasMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace MacacosMazmorrasMVC.Controllers
{
    [Authorize]
    public class SheetCustomController : Controller
    {
        private readonly SheetCustomDAL sheetCustomDal;
        private readonly CampaignDAL campaignDal; //Para obtener las campañas únicas del usuario
        private readonly TypeSheetDAL typeSheetDal; //Para obtener los tipos de ficha que puede crear
        private readonly ImageBBController imageBBController;
        public SheetCustomController(ImageBBDAL imageBBDAL)
        {
            sheetCustomDal = new SheetCustomDAL(Conexion.StringBBDD);
            campaignDal = new CampaignDAL(Conexion.StringBBDD);
            typeSheetDal = new TypeSheetDAL(Conexion.StringBBDD);

            imageBBController = new ImageBBController(imageBBDAL);
        }

        public IActionResult Index()
        {
            //Recovers the id from the user logged in
            int usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            //Change it to show only user custom sheets
            List<SheetCustom> lstSheetCustom = sheetCustomDal.ObtainUserSheets(usuarioId);

            return View(lstSheetCustom);
        }

        public IActionResult NewCharacterForm()
        {
            //Recovers the id from the user logged in
            int usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);


            //Get all the campaigns from the user and store each campaign name
            List<Campaign> userCampaigns = campaignDal.ObtainAllUserCampaigns(usuarioId);
            List<TypeSheet> typeSheetList = typeSheetDal.ObtainAllTypes();

            //Creamos una lista para el dropdown especificando el texto mostrado y el valor real de la selección,
            //en este caso queremos quedarnos con el ID de campaña
            ViewBag.Campaigns = userCampaigns.Select(campaign => 
                                                     new SelectListItem 
                                                     { 
                                                         Text = campaign.CampaignName, 
                                                         Value = campaign.CampaignId.ToString() 
                                                     }).ToList();

            ViewBag.Types = typeSheetList.Select(type =>
                                                     new SelectListItem
                                                     {
                                                         Text = type.TypeSheetClass,
                                                         Value = type.TypeSheetId.ToString()
                                                     }).ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewCharacterForm(SheetCustom newCharacter, IFormFile? imgUrl)
        {
            //// File validation and process for url image
            if (imgUrl != null && imgUrl.Length > 0)
            {
                string url = await imageBBController.UploadImageAsync(imgUrl);
                newCharacter.ImgUrl = url;
            }
            else
                newCharacter.ImgUrl = "https://i.ibb.co/frQkKbr/World.png";

            if (ModelState.IsValid)
            {
                sheetCustomDal.InsertSheet(newCharacter);
                return RedirectToAction("Index", "SheetCustom"); //redirect to SheetCustomList
            }

            return View(newCharacter);
        }

        public IActionResult UpdateCharacterForm(int sheetCustomId)
        {
            SheetCustom sheetToUpdate = sheetCustomDal.ObtainSheetInfo(sheetCustomId);

            //Recovers the id from the user logged in
            int usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            //Get all the campaigns from the user and store each campaign name
            List<Campaign> userCampaigns = campaignDal.ObtainAllUserCampaigns(usuarioId);
            List<TypeSheet> typeSheetList = typeSheetDal.ObtainAllTypes();

            //Creamos una lista para el dropdown especificando el texto mostrado y el valor real de la selección,
            //en este caso queremos quedarnos con el ID de campaña
            ViewBag.Campaigns = userCampaigns.Select(campaign =>
                                                     new SelectListItem
                                                     {
                                                         Text = campaign.CampaignName,
                                                         Value = campaign.CampaignId.ToString()
                                                     }).ToList();

            ViewBag.Types = typeSheetList.Select(type =>
                                                     new SelectListItem
                                                     {
                                                         Text = type.TypeSheetClass,
                                                         Value = type.TypeSheetId.ToString()
                                                     }).ToList();

            return View(sheetToUpdate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCharacterForm(SheetCustom sheetToUpdate, IFormFile? imgUrl)
        {
            //// File validation and process for url image
            if (imgUrl != null && imgUrl.Length > 0)
            {
                string url = await imageBBController.UploadImageAsync(imgUrl);
                sheetToUpdate.ImgUrl = url;
            }
            else
                sheetToUpdate.ImgUrl = "https://i.ibb.co/frQkKbr/World.png";

            if (ModelState.IsValid)
            {
                sheetCustomDal.UpdateSheet(sheetToUpdate);
                return RedirectToAction("Index", "SheetCustom"); //redirect to SheetCustomList
            }

            return View(sheetToUpdate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSheetCustom(int sheetCustomId)
        {
            if (Request.Form["confirmed"] == "true")
            {
                sheetCustomDal.DeleteSheet(sheetCustomId);

                return RedirectToAction("Index", "SheetCustom");
            }

            return NoContent();
        }
    }
}

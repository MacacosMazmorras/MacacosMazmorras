using Microsoft.AspNetCore.Mvc;

namespace MacacosMazmorrasMVC.Controllers
{
    public class CampaignController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NewCampaignForm()
        {
            return View();
        }

        public IActionResult UpdateCampaignForm()
        {
            return View();
        }
    }
}

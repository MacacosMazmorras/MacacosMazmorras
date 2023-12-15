using MacacosMazmorrasMVC.DAL;
using MacacosMazmorrasMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IO;
using System.Text.Json;

namespace MacacosMazmorrasMVC.Controllers
{
    public class ImageBBController : Controller
    {
        private readonly ImageBBDAL _imageBBDAL;

        public ImageBBController(ImageBBDAL imageBBDAL)
        {
            _imageBBDAL = imageBBDAL;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<string> UploadImageAsync(IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    try
                    {
                        await imageFile.CopyToAsync(stream);
                        byte[] imageData = stream.ToArray();
                        string base64String = Convert.ToBase64String(imageData);

                        //obtain directly the ImageBB object
                        ImageBB imageUrl = await _imageBBDAL.UploadImageAsync(base64String);

                        if (imageUrl != null && !string.IsNullOrEmpty(imageUrl.Url))
                        {
                            var model = new ImageBB { Url = imageUrl.Url };
                            return imageUrl.Url;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
            // Handle error
            return null;
        }
    }
}

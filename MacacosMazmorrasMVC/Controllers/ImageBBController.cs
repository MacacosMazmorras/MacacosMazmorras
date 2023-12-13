using MacacosMazmorrasMVC.DAL;
using MacacosMazmorrasMVC.Models;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> UploadImage(IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    try
                    {
                        await imageFile.CopyToAsync(stream);
                        var imageData = stream.ToArray();
                        var base64String = Convert.ToBase64String(imageData);
                        var imageUrl = await _imageBBDAL.UploadImageAsync(base64String);

                        if (imageUrl != null)
                        {
                            var model = new ImageBB { Url = imageUrl };
                            return View("Index", model);
                        }
                    }
                    catch (Exception ex) 
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
            //if (imageUrl != null)
            //{
            //    // Redirigir a la acción Index y pasar la URL como parámetro
            //    return RedirectToAction("Index", new { imageUrl });
            //}

            // Handle error
            return RedirectToAction("Index");
        }
        public IActionResult ShowImage()
        {
            return View();
        }
        public IActionResult ShowImage(string imageUrl)
        {
            // Pasa la URL de la imagen a la vista
            ViewData["url"] = imageUrl;
            return View();
        }

    }
}

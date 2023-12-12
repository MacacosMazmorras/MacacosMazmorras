using MacacosMazmorrasMVC.Models;
using System.Text;

namespace MacacosMazmorrasMVC.DAL
{
    public class ImageBBDAL
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public ImageBBDAL(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["ImgBB:ApiKey"]; //we put the apikey from ImgBB from appsetting.Json
        }

        public async Task<string> UploadImageAsync(string imageData)
        {
            var byteArray = Encoding.UTF8.GetBytes(imageData); // Convertir la cadena a bytes

            var content = new MultipartFormDataContent(); //explicar que es
            content.Add(new ByteArrayContent(byteArray), "image");

            var response = await _httpClient.PostAsync($"https://api.imgbb.com/1/upload?&key={_apiKey}", content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ImageBB>();
                return result.ImageUrl; //alomejor hay que cambiarlo por "url";
            }
            return null;
        }
    }
}

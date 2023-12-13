using MacacosMazmorrasMVC.Models;
using System.Text;
using System.Text.Json;

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

            var response = await _httpClient.PostAsync($"https://api.imgbb.com/1/upload?&key={_apiKey}", content); //llamamos a la api con con la api key
            var responseContent = await response.Content.ReadAsStringAsync();
            //Console.WriteLine($"Response Content: {responseContent}");

            if (responseContent != null)
            {
                var i = JsonSerializer.Deserialize<ImageBB>(responseContent);
                return i.Url;
                //var result = await response.Content.ReadFromJsonAsync<ImageBB>(); // Deserializar la respuesta JSON
                //if (result != null)
                //{
    
                //    Console.WriteLine(result.url);
                //    return result.url;
                //}
            }
            return null;
        }
    }
}

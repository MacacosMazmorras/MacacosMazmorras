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

        public async Task<ImageBB> UploadImageAsync(string imageData)
        {
            var byteArray = Encoding.UTF8.GetBytes(imageData); // Convert chain to bytes

            var content = new MultipartFormDataContent();
            content.Add(new ByteArrayContent(byteArray), "image");

            var response = await _httpClient.PostAsync($"https://api.imgbb.com/1/upload?&key={_apiKey}", content);  //call the api with api key

            //check if response is success
            response.EnsureSuccessStatusCode();

            // Extract the contain from the response
            string responseContent = await response.Content.ReadAsStringAsync();

            ImageBB imageInfo = new ImageBB();

            // Assign the "url" value to ImageBB Url atribut
            imageInfo.Url = GetImageUrlFromJson(responseContent);

            Console.WriteLine(imageInfo.Url);

            return imageInfo;
        }

        private string GetImageUrlFromJson(string responseContent)
        {
            try
            {
                using (JsonDocument document = JsonDocument.Parse(responseContent))
                {
                    JsonElement root = document.RootElement;
                    return root.GetProperty("data").GetProperty("url").GetString();
                }
            }
            catch (JsonException)
            {
                return null;
            }
        }
    }
}

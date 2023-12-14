using System.Text.Json.Serialization;

namespace MacacosMazmorrasMVC.Models
{
    public class ImageBB
    {
        private string url;

        [JsonPropertyName("url")]
        public string Url { get {  return url; } set {  url = value; } }

        public ImageBB() { }
        public ImageBB(string url) 
        {
            Url = url;
        }
    }
}

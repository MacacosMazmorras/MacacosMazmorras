using System.Text.Json.Serialization;

namespace MacacosMazmorrasMVC.Models
{
    public class ImageBB
    {
        private string _url;

        [JsonPropertyName("url")]
        public string Url { get {  return _url; } set {  _url = value; } }

        public ImageBB() { }
        public ImageBB(string _url) 
        {
            Url = _url;
        }
    }
}

namespace MacacosMazmorrasMVC.Models
{
    public class ImageBB
    {
        private string imageUrl;

        public string ImageUrl { get {  return imageUrl; } set {  imageUrl = value; } }

        public ImageBB() { }
        public ImageBB(string imagUrl) 
        {
            ImageUrl = imagUrl;
        }
    }
}

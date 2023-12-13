namespace MacacosMazmorrasMVC.Models
{
    public class Unit
    {
        private string name;
        private int str;
        private int dex;
        private int con;
        private int inte;
        private int wis;
        private int cha;
        private int hp;
        private string ac;
        private string? imgUrl;
        public string Name { get { return name; } set { name = value; } }
        public int Str { get; set; }
        public int Dex { get; set; }
        public int Con { get; set; }
        public int Inte { get; set; }
        public int Wis { get; set; }
        public int Cha { get; set; }
        public int Hp { get; set; }
        public string Ac { get; set; }
        public string? ImgUrl { get; set; }
    }
}

namespace MacacosMazmorrasMVC.Models
{
    public class Unit
    {
        private string name;
        private string raceType;
        private int str;
        private int dex;
        private int con;
        private int inte;
        private int wis;
        private int cha;
        private int hp;
        private int sesionHp;
        private string ac;
        private string? imgUrl;
        private string? actions;
        private bool? isPlayer;
        public string Name { get { return name; } set { name = value; } }
        public string RaceType { get; set; }
        public int Str { get; set; }
        public int Dex { get; set; }
        public int Con { get; set; }
        public int Inte { get; set; }
        public int Wis { get; set; }
        public int Cha { get; set; }
        public int Hp { get; set; }
        public int SesionHp { get { return sesionHp; } set { sesionHp = value; } }
        public string Ac { get; set; }
        public string? ImgUrl { get; set; }
        public string Actions { get; set; }
        public bool? IsPlayer { get {  return isPlayer; } set {  isPlayer = value; } }
    }
}

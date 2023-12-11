namespace MacacosMazmorrasMVC.Models
{
    public class Monster
    {
        private int monsterId;
        private string monsterName;
        private string monsterSize;
        private string monsterType;
        private int monsterAC;
        private int monsterHP;
        private int monsterSpeed;
        private int monsterStr;
        private int monsterDex;
        private int monsterCon;
        private int monsterInt;
        private int monsterWis;
        private int monsterCha;
        private decimal monsterCR;
        private int monsterXP;
        private string? monsterImgUrl;

        public int MonsterId { get { return monsterId; } set { monsterId = value; } }
        public string MonsterName { get {  return monsterName; } set {  monsterName = value; } }
        public string MonsterSize { get {  return monsterSize; } set { monsterSize = value; } }
        public string MonsterType { get {  return monsterType; } set { monsterType = value; } }
        public int MonsterAC { get {  return monsterAC; } set { monsterAC = value; } }
        public int MonsterHP { get {  return monsterHP; } set { monsterHP = value; } }
        public int MonsterSpeed { get {  return monsterSpeed; } set { monsterSpeed = value; } }
        public int MonsterStr { get {  return monsterStr; } set { monsterStr = value; } }
        public int MonsterDex { get { return monsterDex; } set { monsterDex = value; } }
        public int MonsterCon { get { return monsterCon; } set { monsterCon = value; } }
        public int MonsterInt { get { return monsterInt; } set { monsterInt = value; } }
        public int MonsterWis { get { return monsterWis; } set { monsterWis = value; } }
        public int MonsterCha { get { return monsterCha; } set { monsterCha = value; } }
        public decimal MonsterCR { get { return monsterCR; } set { monsterCR = value; } }
        public int MonsterXP { get { return monsterXP; } set { monsterXP = value; } }
        public string? MonsterImgUrl { get { return  monsterImgUrl; } set { monsterImgUrl = value; } }

        public Monster() { }
        public Monster (int monsterId, string monsterName, string monsterSize, string monsterType, int monsterAC, int monsterHP, int monsterSpeed, int monsterStr, int monsterDex, int monsterCon, int monsterInt, int monsterWis, int monsterCha, decimal monsterCR, int monsterXP, string? monsterImgUrl)
        {
            MonsterId = monsterId;
            MonsterName = monsterName;
            MonsterSize = monsterSize;
            MonsterType = monsterType;
            MonsterAC = monsterAC;
            MonsterHP = monsterHP;
            MonsterSpeed = monsterSpeed;
            MonsterStr = monsterStr;
            MonsterDex = monsterDex;
            MonsterCon = monsterCon;
            MonsterInt = monsterInt;
            MonsterWis = monsterWis;
            MonsterCha = monsterCha;
            MonsterCR = monsterCR;
            MonsterXP = monsterXP;
            MonsterImgUrl = monsterImgUrl;
        }
    }
}

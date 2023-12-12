namespace MacacosMazmorrasMVC.Models
{
    public class Monster
    {
        private int monsterId;
        private string monsterName;
        private string monsterType;
        private string monsterAC;
        private int monsterHP;
        private string monsterSpeed;
        private int monsterStr;
        private int monsterDex;
        private int monsterCon;
        private int monsterInt;
        private int monsterWis;
        private int monsterCha;
        private string monsterCR;
        private string? monsterAction;
        private string? monsterImgUrl;

        public int MonsterId { get { return monsterId; } set { monsterId = value; } }
        public string MonsterName { get {  return monsterName; } set {  monsterName = value; } }
        public string MonsterType { get {  return monsterType; } set { monsterType = value; } }
        public string MonsterAC { get {  return monsterAC; } set { monsterAC = value; } }
        public int MonsterHP { get {  return monsterHP; } set { monsterHP = value; } }
        public string MonsterSpeed { get {  return monsterSpeed; } set { monsterSpeed = value; } }
        public int MonsterStr { get {  return monsterStr; } set { monsterStr = value; } }
        public int MonsterDex { get { return monsterDex; } set { monsterDex = value; } }
        public int MonsterCon { get { return monsterCon; } set { monsterCon = value; } }
        public int MonsterInt { get { return monsterInt; } set { monsterInt = value; } }
        public int MonsterWis { get { return monsterWis; } set { monsterWis = value; } }
        public int MonsterCha { get { return monsterCha; } set { monsterCha = value; } }
        public string MonsterCR { get { return monsterCR; } set { monsterCR = value; } }
        public string? MonsterAction { get { return monsterAction; } set { monsterAction = value; } }
        public string? MonsterImgUrl { get { return  monsterImgUrl; } set { monsterImgUrl = value; } }

        public Monster() { }
        public Monster (int monsterId, string monsterName, string monsterType, string monsterAC, int monsterHP, string monsterSpeed, int monsterStr, int monsterDex, int monsterCon, int monsterInt, int monsterWis, int monsterCha, string monsterCR, string? monsterAction, string? monsterImgUrl)
        {
            MonsterId = monsterId;
            MonsterName = monsterName;
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
            MonsterAction = monsterAction;
            MonsterImgUrl = monsterImgUrl;
        }
    }
}

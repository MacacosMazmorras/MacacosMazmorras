namespace MacacosMazmorrasMVC.Models
{
    public class Monster : Unit
    {
        private int monsterId;
        private string monsterType;
        private string monsterSpeed;
        private string monsterCR;
        private string? monsterAction;

        public int MonsterId { get { return monsterId; } set { monsterId = value; } }
        public string MonsterType { get {  return monsterType; } set { monsterType = value; } }
        public string MonsterSpeed { get { return monsterSpeed; } set { monsterSpeed = value; } }
        public string MonsterCR { get { return monsterCR; } set { monsterCR = value; } }
        public string? MonsterAction { get { return monsterAction; } set { monsterAction = value; } }

        public Monster() { }
        public Monster (int monsterId, string monsterName, string monsterType, string monsterAC, int monsterHP, string monsterSpeed, int monsterStr, int monsterDex, int monsterCon, int monsterInt, int monsterWis, int monsterCha, string monsterCR, string? monsterAction, string? monsterImgUrl)
        {
            MonsterId = monsterId;
            Name = monsterName;
            MonsterType = monsterType;
            Ac = monsterAC;
            Hp = monsterHP;
            MonsterSpeed = monsterSpeed;
            Str = monsterStr;
            Dex = monsterDex;
            Con = monsterCon;
            Inte = monsterInt;
            Wis = monsterWis;
            Cha = monsterCha;
            MonsterCR = monsterCR;
            MonsterAction = monsterAction;
            ImgUrl = monsterImgUrl;
        }
    }
}

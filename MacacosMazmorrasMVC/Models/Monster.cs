namespace MacacosMazmorrasMVC.Models
{
    public class Monster : Unit
    {
        private int monsterId;
        private string monsterSpeed;
        private string monsterCR;

        public int MonsterId { get { return monsterId; } set { monsterId = value; } }
        public string MonsterSpeed { get { return monsterSpeed; } set { monsterSpeed = value; } }
        public string MonsterCR { get { return monsterCR; } set { monsterCR = value; } }

        public Monster() { }
        public Monster (int monsterId, string monsterName, string monsterType, string monsterAC, int monsterHP,int sesionHp, string monsterSpeed, int monsterStr, int monsterDex, int monsterCon, int monsterInt, int monsterWis, int monsterCha, string monsterCR, string? monsterAction, string? monsterImgUrl)
        {
            MonsterId = monsterId;
            Name = monsterName;
            RaceType = monsterType;
            Ac = monsterAC;
            Hp = monsterHP;
            SesionHp = sesionHp;
            MonsterSpeed = monsterSpeed;
            Str = monsterStr;
            Dex = monsterDex;
            Con = monsterCon;
            Inte = monsterInt;
            Wis = monsterWis;
            Cha = monsterCha;
            MonsterCR = monsterCR;
            Actions = monsterAction;
            ImgUrl = monsterImgUrl;
        }
    }
}

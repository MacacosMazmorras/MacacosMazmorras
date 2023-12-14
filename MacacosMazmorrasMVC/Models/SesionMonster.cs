namespace MacacosMazmorrasMVC.Models
{
    public class SesionMonster
    {
        private int fKSesionId;
        private int fKMonsterId;
        private int monsterHp;

        public int FKSesionId { get {  return fKSesionId; } set {  fKSesionId = value; } }
        public int FKMonsterId { get { return fKMonsterId;  } set { fKMonsterId = value; } }
        public int MonsterHp { get { return monsterHp; } set { monsterHp = value; } }
        public SesionMonster() { }
        public SesionMonster(int fKSesionId, int fKMonsterId, int monsterHp)
        {
            FKSesionId = fKSesionId;
            FKMonsterId = fKMonsterId;
            MonsterHp = monsterHp;
        }
    }
}

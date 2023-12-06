namespace MacacosMazmorrasMVC.Models
{
    public class SesionMonster
    {
        private int fKSesionId;
        private int fKMonsterId;

        public int FKSesionId { get {  return fKSesionId; } set {  fKSesionId = value; } }
        public int FKMonsterId { get { return fKMonsterId;  } set { fKMonsterId = value; } }

        public SesionMonster() { }
        public SesionMonster(int fKSesionId, int fKMonsterId)
        {
            FKSesionId = fKSesionId;
            FKMonsterId = fKMonsterId;
        }
    }
}

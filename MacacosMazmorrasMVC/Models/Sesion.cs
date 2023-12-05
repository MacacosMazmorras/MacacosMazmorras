namespace MacacosMazmorrasMVC.Models
{
    public class Sesion
    {
        private int sesionId;
        private string sesionName;
        private string? sesionDesc;
        private DateTime? sesionDate;
        private int fKCampaignId;

        public int SesionId { get { return sesionId; } set { sesionId = value; } }
        public string SesionName { get {  return sesionName; } set { sesionName = value; } }
        public string? SesionDesc { get {  return sesionDesc; } set { sesionDesc  = value; } }
        public DateTime? SesionDate { get {  return sesionDate; } set { sesionDate = value; } }
        public int FKCampaignId { get { return fKCampaignId; } set {  fKCampaignId = value; } }

        public Sesion() { }
        public Sesion (int sesionId, string sesionName, string? sesionDesc, DateTime? sesionDate, int fKCampaignId)
        {
            SesionId = sesionId;
            SesionName = sesionName;
            SesionDesc = sesionDesc;
            SesionDate = sesionDate;
            FKCampaignId = fKCampaignId;
        }
    }
}

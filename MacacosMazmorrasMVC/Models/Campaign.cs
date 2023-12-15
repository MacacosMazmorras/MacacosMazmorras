using System.Text.RegularExpressions;

namespace MacacosMazmorrasMVC.Models
{
    public class Campaign
    {
        private int campaignId;
        private string campaignName;
        private string? campaignDesc;
        private string? campaignMap;
        private IFormFile? campaignMapFile;
        private int? fKUsuarioId;

        public int CampaignId { get {  return campaignId; } set {  campaignId = value; } }
        public string CampaignName { get {  return campaignName; } set {  campaignName = value; } }
        public string? CampaignDesc { get {  return campaignDesc; } set { campaignDesc = value; } }
        public string? CampaignMap { get { return campaignMap; } set { campaignMap = value; } }
        public IFormFile? CampaignMapFile { get { return campaignMapFile; } set { campaignMapFile = value; } }
        public int? FKUsuarioId { get { return fKUsuarioId; } set { fKUsuarioId = value; } }

        public Campaign() { }
        public Campaign(string campaignName, string? campaignDesc, string? campaignMap, int? fKUsuarioId)
        {
            CampaignName = campaignName;
            CampaignDesc = campaignDesc;
            CampaignMap = campaignMap;
            FKUsuarioId = fKUsuarioId;
        }

        public Campaign (int campaignId, string campaignName, string? campaignDesc, IFormFile? campaignMapFile, int? fKUsuarioId)
        {
            CampaignId = campaignId;
            CampaignName = campaignName;
            CampaignDesc = campaignDesc;
            CampaignMapFile = campaignMapFile;
            FKUsuarioId = fKUsuarioId;
        }

        //constructor with all atributes
        public Campaign (int campaignId, string campaignName, string? campaignDesc, string? campaignMap, int? fKUsuarioId)
        {
            CampaignId = campaignId;
            CampaignName = campaignName;
            CampaignDesc = campaignDesc;
            CampaignMap = campaignMap;
            FKUsuarioId = fKUsuarioId;
        }
    }
}

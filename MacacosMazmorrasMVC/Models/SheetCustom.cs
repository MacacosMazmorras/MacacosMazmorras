namespace MacacosMazmorrasMVC.Models
{
    public class SheetCustom
    {
        private int sheetCustomId;
        private int fKCampaignId;
        private string sheetCustomName;
        private string? sheetCustomBackground;
        private string? sheetCustomImageUrl;
        private int sheetCustomStr;
        private int sheetCustomDex;
        private int sheetCustomCon;
        private int sheetCustomInt;
        private int sheetCustomWis;
        private int sheetCustomCha;
        private int sheetCustomCA;
        private int sheetCustomPV;
        private int fKTypeSheetId;
        private string? sheetCustomRace;
        private int? sheetCustomCR;
        private int? sheetCustomLevel;

        public int SheetCustomId { get { return sheetCustomId; } set { sheetCustomId = value; } }
        public int FKCampaignId { get { return fKCampaignId;  } set { fKCampaignId = value; } }
        public string SheetCustomName { get { return sheetCustomName; } set { sheetCustomName = value; } }
        public string? SheetCustomBackground { get {  return sheetCustomBackground; } set { sheetCustomBackground = value; } }
        public string? SheetCustomImageUrl { get {  return sheetCustomImageUrl; } set { sheetCustomImageUrl = value; } }
        public int SheetCustomStr { get {  return sheetCustomStr; } set { sheetCustomStr = value; } }
        public int SheetCustomDex { get {  return sheetCustomDex; } set { sheetCustomDex = value; } }
        public int SheetCustomCon {  get { return sheetCustomCon; } set { sheetCustomCon = value; } }
        public int SheetCustomInt { get { return sheetCustomInt; } set { sheetCustomInt = value; } }
        public int SheetCustomWis { get { return sheetCustomWis; } set { sheetCustomWis = value; } }
        public int SheetCustomCha { get { return sheetCustomCha; } set { sheetCustomCha = value; } }
        public int SheetCustomCA {  get { return sheetCustomCA; } set { sheetCustomCA = value; } }
        public int SheetCustomPV { get { return sheetCustomPV; } set { sheetCustomPV = value; } }
        public int FKTypeSheetId {  get { return fKTypeSheetId; } set { fKTypeSheetId = value;} }
        public string? SheetCustomRace { get { return sheetCustomRace; } set { sheetCustomRace = value; } }
        public int? SheetCustomCR { get { return sheetCustomCR; } set { sheetCustomCR = value; } }
        public int? SheetCustomLevel { get { return sheetCustomLevel; } set { sheetCustomLevel = value; } }

        public SheetCustom() { }

        public SheetCustom(int sheetCustomId, int fKCampaignId, string sheetCustomName, string? sheetCustomBackground, string? sheetCustomImageUrl, int sheetCustomStr, int sheetCustomDex, int sheetCustomCon, int sheetCustomInt, int sheetCustomWis, int sheetCustomCha, int sheetCustomCA, int sheetCustomPV, int fKTypeSheetId, string? sheetCustomRace, int? sheetCustomCR, int? sheetCustomLevel)
        {
            SheetCustomId = sheetCustomId;
            FKCampaignId = fKCampaignId;
            SheetCustomName = sheetCustomName;
            SheetCustomBackground = sheetCustomBackground;
            SheetCustomImageUrl = sheetCustomImageUrl;
            SheetCustomStr = sheetCustomStr;
            SheetCustomDex = sheetCustomDex;
            SheetCustomCon = sheetCustomCon;
            SheetCustomInt = sheetCustomInt;
            SheetCustomWis = sheetCustomWis;
            SheetCustomCha = sheetCustomCha;
            SheetCustomCA = sheetCustomCA;
            SheetCustomPV = sheetCustomPV;
            FKTypeSheetId = fKTypeSheetId;
            SheetCustomRace = sheetCustomRace;
            SheetCustomCR = sheetCustomCR;
            SheetCustomLevel = sheetCustomLevel;
        }
    }
}

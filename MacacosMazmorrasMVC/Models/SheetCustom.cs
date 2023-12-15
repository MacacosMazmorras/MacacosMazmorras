namespace MacacosMazmorrasMVC.Models
{
    public class SheetCustom : Unit
    {
        private int sheetCustomId;
        private int fKCampaignId;
        private string? sheetCustomBackground;
        private int fKTypeSheetId;
        private int? sheetCustomCR;
        private int? sheetCustomLevel;

        public int SheetCustomId { get { return sheetCustomId; } set { sheetCustomId = value; } }
        public int FKCampaignId { get { return fKCampaignId;  } set { fKCampaignId = value; } }
        public string? SheetCustomBackground { get {  return sheetCustomBackground; } set { sheetCustomBackground = value; } }
        public int FKTypeSheetId {  get { return fKTypeSheetId; } set { fKTypeSheetId = value;} }
        public int? SheetCustomCR { get { return sheetCustomCR; } set { sheetCustomCR = value; } }
        public int? SheetCustomLevel { get { return sheetCustomLevel; } set { sheetCustomLevel = value; } }

        public SheetCustom() { }

        public SheetCustom(int sheetCustomId, int fKCampaignId, string sheetCustomName, string? sheetCustomBackground, string? sheetCustomImageUrl, int sheetCustomStr, int sheetCustomDex, int sheetCustomCon, int sheetCustomInt, int sheetCustomWis, int sheetCustomCha, string sheetCustomCA, int sheetCustomPV, int sesionHp, int fKTypeSheetId, string? sheetCustomRace, int? sheetCustomCR, int? sheetCustomLevel)
        {
            SheetCustomId = sheetCustomId;
            FKCampaignId = fKCampaignId;
            Name = sheetCustomName;
            SheetCustomBackground = sheetCustomBackground;
            ImgUrl = sheetCustomImageUrl;
            Str = sheetCustomStr;
            Dex = sheetCustomDex;
            Con = sheetCustomCon;
            Inte = sheetCustomInt;
            Wis = sheetCustomWis;
            Cha = sheetCustomCha;
            Ac = sheetCustomCA;
            Hp = sheetCustomPV;
            SesionHp = sesionHp;
            FKTypeSheetId = fKTypeSheetId;
            RaceType = sheetCustomRace;
            SheetCustomCR = sheetCustomCR;
            SheetCustomLevel = sheetCustomLevel;
        }
    }
}

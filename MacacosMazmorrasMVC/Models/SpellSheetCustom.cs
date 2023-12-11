namespace MacacosMazmorrasMVC.Models
{
    public class SpellSheetCustom
    {
        private int fKSheetCustomId;
        private int fKSpellId;

        public int FKSheetCustomId { get { return fKSheetCustomId; } set { fKSheetCustomId = value; } }
        public int FKSpellId { get { return fKSpellId; } set { fKSpellId = value; } }

        public SpellSheetCustom() { }
        public SpellSheetCustom(int fKSheetCustomId, int fKSpellId)
        {
            FKSheetCustomId = fKSheetCustomId;
            FKSpellId = fKSpellId;
        }
    }
}

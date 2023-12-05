namespace MacacosMazmorrasMVC.Models
{
    public class Spell
    {
        private int spellId;
        private string spellName;
        private string[] spellDescription;
        private string spellRange;
        private string spellDuration;
        private string? spellMaterial;
        private bool spellConcentration;
        private int spellLevel;


        public int SpellId { get { return spellId; } set {  spellId = value; } }
        public string SpellName { get {  return spellName; } set { spellName = value; } }
        public string[] SpellDescription { get {  return spellDescription; } set { spellDescription = value; } }
        public string SpellRange { get {  return spellRange; } set { spellRange = value; } }
        public string SpellDuration { get {  return spellDuration; } set { spellDuration = value; } }
        public string? SpellMaterial { get { return spellMaterial; } set { spellMaterial = value; } }
        public bool SpellConcentration { get {  return spellConcentration; } set { spellConcentration = value; } }
        public int SpellLevel { get {  return spellLevel; } set { spellLevel = value; } }

        public Spell() { }

        public Spell(int spellId, string spellName, string[] spellDescription, string spellRange, string spellDuration, string spellMaterial bool spellConcentration, int spellLevel)
        {
            SpellId = spellId;
            SpellName = spellName;
            SpellDescription = spellDescription;
            SpellRange = spellRange;
            SpellDuration = spellDuration;
            SpellMaterial = spellMaterial;
            SpellConcentration = spellConcentration;
            SpellLevel = spellLevel;
        }
    }
}

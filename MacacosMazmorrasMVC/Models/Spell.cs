namespace MacacosMazmorrasMVC.Models
{
    public class Spell
    {
        private int spellId;
        private string spellName;
        private string spellDescription;
        private string spellRange;
        private string spellDuration;
        private bool spellConcentration;
        private int spellLevel;
        private string? spellDamageType;
        private string? spellDamage;

        public int SpellId { get { return spellId; } set {  spellId = value; } }
        public string SpellName { get {  return spellName; } set { spellName = value; } }
        public string SpellDescription { get {  return spellDescription; } set { spellDescription = value; } }
        public string SpellRange { get {  return spellRange; } set { spellRange = value; } }
        public string SpellDuration { get {  return spellDuration; } set { spellDuration = value; } }
        public bool SpellConcentration { get {  return spellConcentration; } set { spellConcentration = value; } }
        public int SpellLevel { get {  return spellLevel; } set { spellLevel = value; } }
        public string? SpellDamageType { get {  return spellDamageType; } set { spellDamageType = value; } }
        public string? SpellDamage { get {  return spellDamage; } set { spellDamage = value; } }

        public Spell() { }

        public Spell(int spellId, string spellName, string spellDescription, string spellRange, string spellDuration, bool spellConcentration, int spellLevel, string? spellDamageType, string? spellDamage)
        {
            SpellId = spellId;
            SpellName = spellName;
            SpellDescription = spellDescription;
            SpellRange = spellRange;
            SpellDuration = spellDuration;
            SpellConcentration = spellConcentration;
            SpellLevel = spellLevel;
            SpellDamageType = spellDamageType;
            SpellDamage = spellDamage;
        }
    }
}

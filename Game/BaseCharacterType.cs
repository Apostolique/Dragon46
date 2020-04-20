namespace GameProject
{
    public class BaseCharacterType
    {
        protected string _name;
        public string Name { get => _name; }

        protected CharacterType _type;
        public CharacterType Type { get => _type; }

        protected int _maxHP;
        public int MaxHP { get => _maxHP; }

        protected int _baseArmour;
        public int BaseArmour { get => _baseArmour; }
        protected int _baseMagicResistance;
        public int BaseMagicResistance { get => _baseMagicResistance; }

        protected int _baseStrength;
        public int BaseStrength { get => _baseStrength; }
        protected int _baseIntelligence;
        public int BaseIntelligence { get => _baseIntelligence; }

        protected string _abilitySound;
        public string AbilitySound { get => _abilitySound; }

        public BaseCharacterType(CharacterType type, int maxHP, string name, int baseArmour, int baseMagicResistance, int baseStrength, int baseIntelligence, string abilitySound)
        {
            _type = type;
            _maxHP = maxHP;
            _name = name;
            _baseArmour = baseArmour;
            _baseStrength = baseStrength;
            _baseIntelligence = baseIntelligence;
            _baseMagicResistance = baseMagicResistance;
            _abilitySound = abilitySound;
        }
    }
}
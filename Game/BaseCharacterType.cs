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

        public BaseCharacterType(CharacterType type, int maxHP, string name, int baseArmour)
        {
            _type = type;
            _maxHP = maxHP;
            _name = name;
            _baseArmour = baseArmour;
        }
    }
}
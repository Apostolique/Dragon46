namespace GameProject
{
    public class HeroType
    {
        protected CharacterType _type;
        public CharacterType Type { get => _type; }

        protected int _maxHP;
        public int MaxHP { get => _maxHP; }

        public HeroType(CharacterType type, int maxHP)
        {
            _type = type;
            _maxHP = maxHP;
        }
    }
}
namespace GameProject
{
    public class EnemyType
    {
        protected CharacterType _type;
        public CharacterType Type { get => _type; }

        protected int _maxHP;
        public int MaxHP { get => _maxHP; }

        public EnemyType(CharacterType type, int maxHP)
        {
            _type = type;
            _maxHP = maxHP;
        }
    }

    public class Enemy
    {
        protected EnemyType _enemyType;
        protected EnemyType EnemyType { get => _enemyType; }
    }
}
namespace GameProject
{
    public class EnemyType : BaseCharacterType
    {
        public EnemyType(CharacterType type, int maxHP, string name) : base(type, maxHP, name)
        {

        }
    }

    public class Enemy
    {
        protected EnemyType _enemyType;
        public EnemyType EnemyType { get => _enemyType; }
    }
}
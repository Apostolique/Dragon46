namespace GameProject
{
    public class EnemyType : BaseCharacterType
    {
        public EnemyType(CharacterType type, int maxHP, string name, int baseArmour) : base(type, maxHP, name, baseArmour)
        {

        }
    }

    public class Enemy
    {
        protected EnemyType _enemyType;
        public EnemyType EnemyType { get => _enemyType; }
    }
}
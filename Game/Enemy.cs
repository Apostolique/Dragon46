namespace GameProject
{
    public class EnemyType : BaseCharacterType
    {
        protected int _difficultyScore;
        public int DifficultyScore { get => _difficultyScore; }

        public EnemyType(CharacterType type, int maxHP, string name, int baseArmour, int difficultyScore) : base(type, maxHP, name, baseArmour)
        {
            _difficultyScore = difficultyScore;
        }
    }

    public class Enemy
    {
        protected EnemyType _enemyType;
        public EnemyType EnemyType { get => _enemyType; }

        public Enemy(EnemyType enemyType)
        {
            _enemyType = enemyType;
        }
    }
}
namespace GameProject
{
    public class Character
    {
        protected int _maxHP;
        protected int _currentHP;
        public int CurrentHP { get => _currentHP; }

        protected bool _dead;
        public bool Dead { get => _dead; }

        protected AbilityTimer _castingAbility;
        public AbilityTimer CastingAbility { get => _castingAbility; }

        public Character() { }

        public void ApplyDamage(int damage)
        {
            if (_dead)
                return;

            _currentHP -= damage;
            if (_currentHP < 0)
            {
                _currentHP = 0;
                _dead = true;
            }
        }

        public void ApplyHeal(int heal)
        {
            if (_dead)
                return;

            _currentHP += heal;
            if (_currentHP > _maxHP)
                _currentHP = _maxHP;
        }
    }
}

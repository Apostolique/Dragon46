using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameProject
{
    public class Character
    {
        protected CharacterType _type;
        public CharacterType Type { get => _type; }

        public CharacterSprite Sprite;

        protected int _maxHP;
        protected int _currentHP;
        public int CurrentHP { get => _currentHP; }

        protected bool _dead;
        public bool Dead { get => _dead; }

        protected bool _enemy;
        public bool Enemy { get => _enemy; }

        public int Slot;

        protected AbilityTimer _castingAbility;
        public AbilityTimer CastingAbility { get => _castingAbility; }

        protected StatusEffect _statusEffect;
        public StatusEffect StatusEffect { get => _statusEffect; }

        public Character(CharacterType type, bool enemy, int slot)
        {
            _type = type;
            _enemy = enemy;
            Slot = slot;
        }

        public void Update(GameTime gameTime)
        {
            _castingAbility?.Update(gameTime);
            if (_castingAbility != null && _castingAbility.Finished)
                _castingAbility = null;

            _statusEffect?.Update(gameTime);
            if (_statusEffect != null && _statusEffect.Finished)
                _statusEffect = null;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {

        }

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

        public void AbilityFinished()
        {
            _castingAbility = null;
        }

        public void CastAbility(AbilityTimer castAbility)
        {
            if (_castingAbility != null)
                return;

            _castingAbility = castAbility;
        }

        public void ApplyStatusEffect(StatusEffectType type)
        {
            if (_statusEffect != null || !_statusEffect.Finished)
                return;

            var e = Database.GetStatusEffect(type);
            _statusEffect = (StatusEffect)Activator.CreateInstance(e, this);
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameProject
{
    public class Character
    {
        protected string _name;
        public string Name { get => _name; }

        protected CharacterType _type;
        public CharacterType Type { get => _type; }

        public CharacterSprite Sprite;

        protected Vector2 _drawPosition = Vector2.Zero;
        public Vector2 DrawPosition { get => _drawPosition; }

        protected const int _hitDuration = 600;
        protected bool _isHit;
        protected int _hitTimer;

        protected int _maxHP;
        protected int _baseArmour;
        protected int _baseMagicResistance;
        protected int _currentHP;
        protected int _baseStrength;
        protected int _baseIntelligence;
        public int MaxHP { get => _maxHP; }
        public int CurrentHP { get => _currentHP; }
        public int BaseArmour { get => _baseArmour; }
        public int BaseMagicResistance { get => _baseMagicResistance; }
        public int BaseStrength { get => _baseStrength; }
        public int BaseIntelligence { get => _baseIntelligence; }

        public int AddedArmour { get; set; }
        public int AddedMagicResistance { get; set; }
        public int AddedStrength { get; set; }
        public int AddedIntelligence { get; set; }

        protected bool _dead;
        public bool Dead { get => _dead; }

        protected bool _enemy;
        public bool Enemy { get => _enemy; }

        protected bool _player;
        public bool Player { get => _player; }

        public int Slot;

        protected AbilityTimer _castingAbility;
        public AbilityTimer CastingAbility { get => _castingAbility; }
        public bool IsCasting { get => (_castingAbility == null || _castingAbility.Finished) ? false : true; }

        //protected int _castingCooldownDuration = 600;
        protected int _castingCooldownTimer;
        public bool CastingCooldown { get => _castingCooldownTimer > 0; }

        protected List<StatusEffect> _statusEffects;
        public List<StatusEffect> StatusEffects { get => _statusEffects; }

        protected List<Buff> _buffs;
        public List<Buff> Buffs { get => _buffs; }

        public Character(BaseCharacterType data, bool enemy, int slot, Vector2 drawPosition, bool player = false)
        {
            _enemy = enemy;
            _type = data.Type;
            _maxHP = data.MaxHP;
            _currentHP = _maxHP;
            _drawPosition = drawPosition;
            _name = data.Name;
            Slot = slot;
            _player = player;
            _baseStrength = data.BaseStrength;
            _baseIntelligence = data.BaseIntelligence;

            _baseArmour = data.BaseArmour;
            _baseMagicResistance = data.BaseMagicResistance;

            Sprite = Assets.CharacterSprites[_type];

            _buffs = new List<Buff>();
            _statusEffects = new List<StatusEffect>();
        }

        public void Update(GameTime gameTime)
        {
            if (_dead)
                return;

            _castingAbility?.Update(gameTime);
            if (_castingAbility != null && _castingAbility.Finished)
                _castingAbility = null;

            for (var i = 0; i < _statusEffects.Count; i++)
                _statusEffects[i]?.Update(gameTime);

            _statusEffects.RemoveAll(s => s.Finished);

            for (var i = 0; i < _buffs.Count; i++)
                _buffs[i].Update(gameTime);

            _buffs.RemoveAll(b => b.Finished);

            if (_isHit)
            {
                _hitTimer -= gameTime.ElapsedGameTime.Milliseconds;
                if (_hitTimer <= 0)
                    _isHit = false;
            }

            if (_castingCooldownTimer > 0)
                _castingCooldownTimer -= gameTime.ElapsedGameTime.Milliseconds;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_isHit ? Sprite.HitTexture : Sprite.Texture, _drawPosition, Color.White);
        }

        public void ApplyDamage(DamageType damageType, int damage)
        {
            if (_dead)
                return;
            if (damage <= 0)
                return;

            var damageAfterArmour = damage;

            if (damageType == DamageType.Physical)
            {
                float appliedArmour = BaseArmour + AddedArmour;

                if (appliedArmour > 80)
                    appliedArmour = 80;

                damageAfterArmour = (int)(damage - (damage * (appliedArmour / 100f)));

                if (damageAfterArmour < 1)
                    damageAfterArmour = 1;
            }
            else if (damageType == DamageType.Magical)
            {
                float appliedArmour = BaseMagicResistance + AddedMagicResistance;

                if (appliedArmour > 80)
                    appliedArmour = 80;

                damageAfterArmour = (int)(damage - (damage * (appliedArmour / 100f)));

                if (damageAfterArmour < 1)
                    damageAfterArmour = 1;
            }

            _currentHP -= damageAfterArmour;
            if (_currentHP <= 0)
            {
                _currentHP = 0;
                _dead = true;
            }
            else
            {
                _hitTimer = _hitDuration;
                _isHit = true;
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

        public bool ApplyBuff(Buff buff)
        {
            var existingBuff = _buffs.Where(e => e.Name == buff.Name).FirstOrDefault();
            if (existingBuff != null)
            {
                existingBuff.ResetTimer();
                return false;
            }

            buff.Apply();
            _buffs.Add(buff);
            return true;
        }

        public void AbilityFinished(Ability ability)
        {
            _castingAbility = null;
            _castingCooldownTimer = ability.CooldownDuration;
        }

        public void CastAbility(AbilityTimer castAbility)
        {
            if (_castingAbility != null)
                return;

            _castingAbility = castAbility;
        }

        public bool ApplyStatusEffect(StatusEffect effect)
        {
            var existingEffect = _statusEffects.Where(e => e.Type == effect.Type).FirstOrDefault();
            if (existingEffect != null)
            {
                existingEffect.ResetTimer();
                return false;
            }

            _statusEffects.Add(effect);
            return true;
        }
    }
}

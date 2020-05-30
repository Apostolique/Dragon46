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

        public int CollisionOrder = 0;

        protected CharacterType _type;
        public CharacterType Type { get => _type; }

        public CharacterSprite Sprite;

        protected Vector2 _drawPosition = Vector2.Zero;
        public Vector2 DrawPosition { get => _drawPosition; }

        public bool Hover;
        public Vector2 CollisionOffset;

        protected Tween _scaleTween;
        protected Tween _jumpUpTween;
        protected Tween _jumpDownTween;

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

        protected int _silenceTimer;
        public bool Silenced;

        protected List<StatusEffect> _statusEffects;
        public List<StatusEffect> StatusEffects { get => _statusEffects; }

        protected List<Buff> _buffs;
        public List<Buff> Buffs { get => _buffs; }

        protected string _abilitySound;

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
            _abilitySound = data.AbilitySound;

            _baseArmour = data.BaseArmour;
            _baseMagicResistance = data.BaseMagicResistance;

            Sprite = Assets.CharacterSprites[_type];

            _buffs = new List<Buff>();
            _statusEffects = new List<StatusEffect>();

            _scaleTween = new Tween(1200 + Core.R.Next(0, 500), 1f, 0.95f, EasingFunctions.ExponentialInOut, true);
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

            _scaleTween.Update(gameTime);
            if (_jumpUpTween != null)
                if (_jumpUpTween.Update(gameTime))
                    _jumpUpTween = null;
            if (_jumpUpTween == null && _jumpDownTween != null)
                if (_jumpDownTween.Update(gameTime))
                    _jumpDownTween = null;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var texture = _isHit ? Sprite.HitTexture : Sprite.Texture;
            var jump = Vector2.Zero;
            if (_jumpUpTween != null)
                jump = new Vector2(0, _jumpUpTween.Value);
            else if (_jumpDownTween != null)
                jump = new Vector2(0, _jumpDownTween.Value);

            Vector2 origin = new Vector2(texture.Width / 2, texture.Height);
            if (Type == CharacterType.Dragon)
                origin = new Vector2(0 + 250, texture.Height + 250);

            spriteBatch.Draw(texture, _drawPosition + jump + new Vector2(0, texture.Height), null, Sprite.Colour, 0, origin, _scaleTween.Value, SpriteEffects.None, 0);
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

            ScreenEffectsManager.AddScreenText(new ScreenText()
            {
                Text = damageAfterArmour.ToString(),
                Colour = Color.Red,
                Duration = 3500,
                Position = _drawPosition - new Vector2(0, 75),
                Velocity = new Vector2(0, -30f)
            });

            ScreenEffectsManager.AddCharacterFadeEffect(new CharacterFadeEffect()
            {
                TargetTransparency = 80,
                Duration = 100,
                Character = this,
            });

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

            ScreenEffectsManager.AddScreenText(new ScreenText()
            {
                Text = heal.ToString(),
                Colour = Color.LightGreen,
                Duration = 3500,
                Position = _drawPosition - new Vector2(0, 75),
                Velocity = new Vector2(0, -30f)
            });

            _currentHP += heal;
            if (_currentHP > _maxHP)
                _currentHP = _maxHP;
        }

        public void ApplySilence()
        {
            if (Silenced)
                return;

            Silenced = true;
            _castingAbility = null;
        }

        public void RemoveSilence()
        {
            Silenced = false;
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

        public void CancelCasting()
        {
            _castingAbility = null;
        }

        public void AbilityFinished(AbilityTimer abilityTimer)
        {
            _castingAbility = null;
            _castingCooldownTimer = abilityTimer.Ability.CooldownDuration;

            Assets.SoundManager.PlaySound(_abilitySound, (int)SoundType.SFX);
            ScreenEffectsManager.AddAbilitySpecialEffect(abilityTimer);
        }

        public void CastAbility(AbilityTimer castAbility)
        {
            if (_castingAbility != null)
                return;
            if (Silenced)
                return;

            _castingAbility = castAbility;

            _jumpUpTween = new Tween(500, 0, -50, EasingFunctions.ElasticIn);
            _jumpDownTween = new Tween(500, -50, 0, EasingFunctions.BounceOut);
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

        public bool PointInCharacter(Vector2 point)
        {
            var characterRect = new Rectangle(
                (int)_drawPosition.X + (int)CollisionOffset.X - (Sprite.Texture.Width / 2),
                (int)_drawPosition.Y + (int)CollisionOffset.Y,
                Sprite.Texture.Width - (int)CollisionOffset.X,
                Sprite.Texture.Height - (int)CollisionOffset.Y
            );
            
            if (point.X < characterRect.X)
                return false;
            if (point.Y < characterRect.Y)
                return false;
            if (point.X > (characterRect.X + characterRect.Width))
                return false;
            if (point.Y > (characterRect.Y + characterRect.Height))
                return false;

            return true;
        }
    }
}

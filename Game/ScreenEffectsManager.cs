using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpriteFontPlus;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameProject
{
    public enum FadeDirection
    {
        None = 0,
        FadeIn = 1,
        FadeOut = -1
    };

    public class ScreenText
    {
        public string Text;
        public Color Colour;
        public int Duration;
        public bool Finished;
        public Vector2 Position;
        public Vector2 Velocity;
        public int TextSize = 20;
    }

    public class CharacterFadeEffect
    {
        public Character Character;
        public bool Finished;
        public int Duration;
        public float TargetTransparency;

        protected FadeDirection _fadeDirection = FadeDirection.None;
        protected float _fadeTransparency = 0.0f;
        protected float _fadeChangePerSecond = 0.0f;

        protected bool _firstStageFinished;

        protected Color _startColour;
        public Color Colour { get => Character.Sprite.Colour; }

        public void Start(bool secondStage = false)
        {
            if ((byte)TargetTransparency == Colour.A)
                return;

            _fadeDirection = FadeDirection.FadeIn;

            if (TargetTransparency < (float)Colour.A)
                _fadeDirection = FadeDirection.FadeOut;

            _fadeTransparency = Colour.A;

            float totalFadeChange = TargetTransparency - Colour.A;
            if (totalFadeChange < 0)
                totalFadeChange *= -1;

            _fadeChangePerSecond = totalFadeChange / (Duration / 1000.0f);

            if (!secondStage)
            {
                _startColour = Character.Sprite.Colour;
                Character.Sprite.Colour = Color.Red;
            }
        }

        public void Update(GameTime gameTime)
        {
            if (Finished)
                return;
            if (_fadeDirection == FadeDirection.None)
                return;

            _fadeTransparency += (_fadeChangePerSecond * ((float)gameTime.ElapsedGameTime.Milliseconds / 1000.0f)) * (float)_fadeDirection;

            var stageFinished = false;

            if (_fadeDirection == FadeDirection.FadeIn && _fadeTransparency >= TargetTransparency)
            {
                _fadeTransparency = TargetTransparency;
                _fadeDirection = FadeDirection.None;
                stageFinished = true;
            }
            else if (_fadeDirection == FadeDirection.FadeOut && _fadeTransparency <= TargetTransparency)
            {
                _fadeTransparency = TargetTransparency;
                _fadeDirection = FadeDirection.None;
                stageFinished = true;
            }

            Character.Sprite.Colour.A = (byte)_fadeTransparency;

            if (stageFinished)
            {
                if (!_firstStageFinished)
                {
                    TargetTransparency = 255;
                    _firstStageFinished = true;
                    Start(true);
                }
                else
                {
                    Finished = true;
                    _fadeDirection = FadeDirection.None;
                    Character.Sprite.Colour = _startColour;
                }
            }
        }
    }

    public enum AbilitySpecialEffectType
    {
        Shield,
        Slash,
        Fireball,
        Fire,
        Poison,
        Magic,
        Arrow,
        Heal
    }

    public class AbilitySpecialEffect
    {
        public int Duration;
        public bool Finished;
        public float Scale = 1f;
        public float Rotation = 0f;

        public Character Target;

        public virtual void Start() { }
        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(SpriteBatch spriteBatch) { }
    }

    public class ShieldAbilityEffect : AbilitySpecialEffect
    {
        public Texture2D Texture;
        public Vector2 Offset;

        protected float _fadeTransparency = 255f;
        protected float _fadeChangePerSecond = 0.0f;
        protected Color _colour = Color.White;

        protected Vector2 _position;

        public override void Start()
        {
            _position.X = Target.DrawPosition.X + (Target.Sprite.Texture.Width / 2) - (Texture.Width / 2) - (Target.Sprite.Texture.Width / 2);
            _position.Y = Target.DrawPosition.Y + (Target.Sprite.Texture.Height / 2) - (Texture.Height / 2);

            if (Target.Type == CharacterType.Dragon)
            {
                _position.Y -= 250;
                _position.X += 100;
            }

            float totalFadeChange = 255f;

            _fadeChangePerSecond = totalFadeChange / (Duration / 1000.0f);
        }

        public override void Update(GameTime gameTime)
        {
            if (Finished)
                return;

            _fadeTransparency -= _fadeChangePerSecond * ((float)gameTime.ElapsedGameTime.Milliseconds / 1000.0f);

            if (_fadeTransparency <= 0)
            {
                _fadeTransparency = 0;
                Finished = true;
            }

            _colour.A = (byte)_fadeTransparency;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, _position + Offset, null, _colour, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
        }
    }

    public class HealAbilityEffect : AbilitySpecialEffect
    {
        protected Random _rng = new Random();

        public Texture2D ParticleTexture;

        protected int _particles;
        protected int _particlesCreated;
        protected int _timePerParticle;
        protected int _timePerParticleCounter;

        public Vector2 SpawnPosition;

        public override void Start()
        {
            _particles = _rng.Next(10, 15);
            _timePerParticle = Duration / _particles;
            _timePerParticleCounter = _timePerParticle;

            SpawnPosition.X = Target.DrawPosition.X + (Target.Sprite.Texture.Width / 2) - (Target.Sprite.Texture.Width / 2);
            SpawnPosition.Y = Target.DrawPosition.Y + (Target.Sprite.Texture.Height / 2);
        }

        public override void Update(GameTime gameTime)
        {
            _timePerParticleCounter -= gameTime.ElapsedGameTime.Milliseconds;

            if (_timePerParticleCounter <= 0)
            {
                _timePerParticleCounter = _timePerParticle;
                _particlesCreated += 1;

                ScreenEffectsManager.AddScreenParticle(new ScreenParticle(ParticleTexture)
                {
                    Duration = Duration,
                    Position = SpawnPosition + new Vector2(_rng.Next(-50, 50), _rng.Next(-50, 50)),
                    Velocity = new Vector2(0, -75f),
                });
            }

            if (_particlesCreated == _particles)
                Finished = true;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

        }
    }

    public class MagicProjectileEffect : AbilitySpecialEffect
    {
        protected Random _rng = new Random();

        public Texture2D ProjectileTexture;
        public Texture2D ParticleTexture;
        public Color Colour = Color.White;

        public Vector2 Position;
        public Vector2 Velocity;
        public Vector2 ProfectileOffset = new Vector2(5, -125);

        protected int _particles;
        protected int _particlesCreated;
        protected int _timePerParticle;
        protected int _timePerParticleCounter;

        public override void Start()
        {
            _particles = _rng.Next(20, 30);
            _timePerParticle = Duration / _particles;
            _timePerParticleCounter = _timePerParticle;

            Position.X = Target.DrawPosition.X - 50;
            Position.Y = Target.DrawPosition.Y - 50;
        }

        public override void Update(GameTime gameTime)
        {
            if (Finished)
                return;

            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += (Velocity * delta);

            Duration -= gameTime.ElapsedGameTime.Milliseconds;

            _timePerParticleCounter -= gameTime.ElapsedGameTime.Milliseconds;

            if (_timePerParticleCounter <= 0 && _particlesCreated < _particles)
            {
                _timePerParticleCounter = _timePerParticle;
                _particlesCreated += 1;

                ScreenEffectsManager.AddScreenParticle(new ScreenParticle(ParticleTexture)
                {
                    Duration = Duration,
                    Position = Position + ProfectileOffset + new Vector2(_rng.Next(-8, 8), _rng.Next(-8, 8)),
                    Velocity = new Vector2(-25, -75f),
                    Scale = 2f,
                });
            }

            if (Duration <= 0)
            {
                Duration = 0;
                Finished = true;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ProjectileTexture, Position, null, Colour, Rotation, Vector2.Zero, Scale, SpriteEffects.None, 0f);
        }
    }

    public class ScreenParticle
    {
        public float Scale = 1f;
        public float Rotation = 0f;
        public Texture2D Texture;
        public bool Finished;

        public int Duration;
        public Color Colour = Color.White;

        public Vector2 Position;
        public Vector2 Velocity;
        public Vector2 Origin;

        public ScreenParticle(Texture2D texture)
        {
            Texture = texture;

            Origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
        }

        public void Update(GameTime gameTime)
        {
            if (Finished)
                return;

            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position += (Velocity * delta);

            Duration -= gameTime.ElapsedGameTime.Milliseconds;

            if (Duration <= 0)
            {
                Duration = 0;
                Finished = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Colour, Rotation, Origin, Scale, SpriteEffects.None, 0f);
        }
    }

    public static class ScreenEffectsManager
    {
        private static List<ScreenText> _screenText = new List<ScreenText>();
        private static List<CharacterFadeEffect> _characterFadeEffects = new List<CharacterFadeEffect>();

        private static List<AbilitySpecialEffect> _abilityEffects = new List<AbilitySpecialEffect>();
        private static List<ScreenParticle> _screenParticles = new List<ScreenParticle>();

        public static void AddScreenParticle(ScreenParticle screenParticle)
        {
            _screenParticles.Add(screenParticle);
        }

        public static void AddScreenText(ScreenText screenText)
        {
            _screenText.Add(screenText);
        }

        public static void AddCharacterFadeEffect(CharacterFadeEffect fadeEffect)
        {
            if (_characterFadeEffects.Where(e => e.Character.Name == fadeEffect.Character.Name).Count() > 0)
                return;

            fadeEffect.Start();
            _characterFadeEffects.Add(fadeEffect);
        }

        public static void AddAbilitySpecialEffect(AbilityTimer abilityTimer)
        {
            AbilitySpecialEffect newEffect = null;

            switch (abilityTimer.Ability.AbilityType)
            {
                case AbilityType.Barrier:
                case AbilityType.BruteDefend:
                case AbilityType.WarriorDefend:
                case AbilityType.WizardBarrier:
                    {
                        newEffect = new ShieldAbilityEffect()
                        {
                            Target = abilityTimer.Target,
                            Duration = abilityTimer.Ability.CooldownDuration,
                            Texture = Assets.AbilitySpecialEffectTextures[AbilitySpecialEffectType.Shield]
                        };
                        newEffect.Start();
                    }
                    break;

                case AbilityType.MinionSpear:
                case AbilityType.BruteSmash:
                case AbilityType.WizardStaff:
                case AbilityType.WarriorSword:
                case AbilityType.WarriorShieldBash:
                case AbilityType.DragonStun:
                case AbilityType.BruteStun:
                case AbilityType.ShamanStaff:
                    {
                        {
                            newEffect = new ShieldAbilityEffect()
                            {
                                Target = abilityTimer.Target,
                                Duration = (int)(abilityTimer.Ability.CooldownDuration * 0.6),
                                Texture = Assets.AbilitySpecialEffectTextures[AbilitySpecialEffectType.Slash],
                                Scale = 2f,
                                Offset = new Vector2(-50, -50),
                            };
                            newEffect.Start();
                        }
                    }
                    break;

                case AbilityType.DragonHeal:
                case AbilityType.HealingLight:
                case AbilityType.ShamanHeal:
                case AbilityType.BruteRegen:
                case AbilityType.MinionRegen:
                case AbilityType.Regeneration:
                    {
                        newEffect = new HealAbilityEffect()
                        {
                            Target = abilityTimer.Target,
                            Duration = abilityTimer.Ability.CooldownDuration,
                            ParticleTexture = Assets.AbilitySpecialEffectTextures[AbilitySpecialEffectType.Heal],
                        };
                        newEffect.Start();
                    }
                    break;

                case AbilityType.WizardStun:
                case AbilityType.DragonRoar:
                case AbilityType.Distract:
                case AbilityType.Silence:
                    {
                        newEffect = new MagicProjectileEffect()
                        {
                            Target = abilityTimer.Target,
                            Duration = abilityTimer.Ability.CooldownDuration,
                            ProjectileTexture = Assets.AbilitySpecialEffectTextures[AbilitySpecialEffectType.Fireball],
                            ParticleTexture = Assets.AbilitySpecialEffectTextures[AbilitySpecialEffectType.Magic],
                            Colour = new Color(0, 0, 120, 120),
                            Velocity = new Vector2(75f, 300f),
                            Rotation = 180f,
                        };
                        newEffect.Start();
                    }
                    break;

                case AbilityType.WizardFireball:
                case AbilityType.ShamanFireball:
                case AbilityType.DragonFire:
                    {
                        newEffect = new MagicProjectileEffect()
                        {
                            Target = abilityTimer.Target,
                            Duration = abilityTimer.Ability.CooldownDuration,
                            ProjectileTexture = Assets.AbilitySpecialEffectTextures[AbilitySpecialEffectType.Fireball],
                            ParticleTexture = Assets.AbilitySpecialEffectTextures[AbilitySpecialEffectType.Fire],
                            Colour = Color.White,
                            Velocity = new Vector2(75f, 300f),
                            Rotation = 180f,
                        };
                        newEffect.Start();
                    }
                    break;

                case AbilityType.ShamanPoison:
                    {
                        newEffect = new MagicProjectileEffect()
                        {
                            Target = abilityTimer.Target,
                            Duration = abilityTimer.Ability.CooldownDuration,
                            ProjectileTexture = Assets.AbilitySpecialEffectTextures[AbilitySpecialEffectType.Fireball],
                            ParticleTexture = Assets.AbilitySpecialEffectTextures[AbilitySpecialEffectType.Poison],
                            Colour = Color.Green,
                            Velocity = new Vector2(75f, 300f),
                            Rotation = 180f,
                        };
                        newEffect.Start();
                    }
                    break;

                case AbilityType.ArrowShot:
                    {
                        newEffect = new MagicProjectileEffect()
                        {
                            Target = abilityTimer.Target,
                            Duration = abilityTimer.Ability.CooldownDuration,
                            ProjectileTexture = Assets.AbilitySpecialEffectTextures[AbilitySpecialEffectType.Arrow],
                            ParticleTexture = Assets.AbilitySpecialEffectTextures[AbilitySpecialEffectType.Fire],
                            Colour = Color.White,
                            Velocity = new Vector2(75f, 300f),
                            Rotation = -250f,
                            Scale = 0.5f,
                            ProfectileOffset = new Vector2(0, 0)
                        };
                        newEffect.Start();
                    }
                    break;

                case AbilityType.SilenceArrow:
                case AbilityType.StunArrow:
                    {
                        newEffect = new MagicProjectileEffect()
                        {
                            Target = abilityTimer.Target,
                            Duration = abilityTimer.Ability.CooldownDuration,
                            ProjectileTexture = Assets.AbilitySpecialEffectTextures[AbilitySpecialEffectType.Arrow],
                            ParticleTexture = Assets.AbilitySpecialEffectTextures[AbilitySpecialEffectType.Magic],
                            Colour = Color.White,
                            Velocity = new Vector2(75f, 300f),
                            Rotation = -250f,
                            Scale = 0.5f,
                            ProfectileOffset = new Vector2(0, 0)
                        };
                        newEffect.Start();
                    }
                    break;

                case AbilityType.PoisonArrow:
                    {
                        newEffect = new MagicProjectileEffect()
                        {
                            Target = abilityTimer.Target,
                            Duration = abilityTimer.Ability.CooldownDuration,
                            ProjectileTexture = Assets.AbilitySpecialEffectTextures[AbilitySpecialEffectType.Arrow],
                            ParticleTexture = Assets.AbilitySpecialEffectTextures[AbilitySpecialEffectType.Poison],
                            Colour = Color.White,
                            Velocity = new Vector2(75f, 300f),
                            Rotation = -250f,
                            Scale = 0.5f,
                            ProfectileOffset = new Vector2(0, 0)
                        };
                        newEffect.Start();
                    }
                    break;
            }

            if (newEffect != null)
                _abilityEffects.Add(newEffect);
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            for (var i = 0; i < _screenText.Count; i++)
            {
                var screenText = _screenText[i];
                Assets.UIFont.Size = screenText.TextSize;
                spriteBatch.DrawString(Assets.UIFont, screenText.Text, screenText.Position, screenText.Colour);
            }

            for (var i = 0; i < _abilityEffects.Count; i++)
                _abilityEffects[i]?.Draw(spriteBatch);

            for (var i = 0; i < _screenParticles.Count; i++)
                _screenParticles[i]?.Draw(spriteBatch);
        }

        public static void Update(GameTime gameTime)
        {
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            for (var i = 0; i < _screenText.Count; i++)
            {
                var screenText = _screenText[i];
                screenText.Position += (screenText.Velocity * delta);
                screenText.Duration -= gameTime.ElapsedGameTime.Milliseconds;

                if (screenText.Duration <= 0)
                    screenText.Finished = true;
            }

            for (var i = 0; i < _characterFadeEffects.Count; i++)
                _characterFadeEffects[i]?.Update(gameTime);

            for (var i = 0; i < _abilityEffects.Count; i++)
                _abilityEffects[i]?.Update(gameTime);

            for (var i = 0; i < _screenParticles.Count; i++)
                _screenParticles[i]?.Update(gameTime);

            _screenText.RemoveAll(t => t.Finished);
            _characterFadeEffects.RemoveAll(e => e.Finished);
            _abilityEffects.RemoveAll(e => e.Finished);
            _screenParticles.RemoveAll(p => p.Finished);
        }
    }
}
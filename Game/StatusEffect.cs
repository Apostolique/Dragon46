using Microsoft.Xna.Framework;

namespace GameProject
{
    public class StatusEffect
    {
        protected Character _owner;

        protected int _timeRemaining;
        public int TimeRemaining { get => _timeRemaining; }

        protected bool _finished;
        public bool Finished { get => _finished; }

        public StatusEffect(Character owner)
        {
            _owner = owner;
        }

        public virtual bool Update(GameTime gameTime)
        {
            if (_finished)
                return _finished;

            _timeRemaining -= gameTime.ElapsedGameTime.Milliseconds;

            if (_timeRemaining < 0)
                _finished = true;

            return _finished;
        }
    }

    public class StatusEffectOverTime : StatusEffect
    {
        protected int _tickDuration;
        protected int _currentTickCounter;

        public StatusEffectOverTime(Character owner) : base(owner) { }

        public override bool Update(GameTime gameTime)
        {
            if (_finished)
                return _finished;

            _currentTickCounter += gameTime.ElapsedGameTime.Milliseconds;
            if (_currentTickCounter >= _tickDuration)
            {
                _currentTickCounter = 0;
                Tick();
            }

            return base.Update(gameTime);
        }

        public virtual void Tick() { }
    }

    public class PoisonStatus : StatusEffectOverTime
    {
        protected int _damagePerTick;

        public PoisonStatus(Character owner) : base(owner) { }

        public override void Tick()
        {
            _owner.ApplyDamage(_damagePerTick);
        }
    }

    public class HealOverTimeStatus : StatusEffectOverTime
    {
        protected int _healPerTick;

        public HealOverTimeStatus(Character owner) : base(owner) { }

        public override void Tick()
        {
            _owner.ApplyHeal(_healPerTick);
        }
    }
}
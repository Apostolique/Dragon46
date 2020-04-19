using Microsoft.Xna.Framework;

namespace GameProject
{
    public class StatusEffect
    {
        public Character Owner;

        protected int _totalDuration;
        protected int _timeRemaining;
        public int TimeRemaining { get => _timeRemaining; }

        protected bool _finished;
        public bool Finished { get => _finished; }

        protected string _name;
        public string Name { get => _name; }

        public StatusEffectType Type { get; set; }

        public StatusEffect(Character owner, int duration)
        {
            _totalDuration = duration;
            _timeRemaining = duration;
            Owner = owner;
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

        public virtual void ResetTimer()
        {
            _timeRemaining = _totalDuration;
        }
    }

    public class StatusEffectOverTime : StatusEffect
    {
        protected int _tickDuration;
        protected int _currentTickCounter;

        public StatusEffectOverTime(Character owner, int duration) : base(owner, duration) { }

        public void Start(int tickDuration)
        {
            _tickDuration = tickDuration;
            _currentTickCounter = tickDuration;
        }

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
        public int DamagePerTick;

        public PoisonStatus(Character owner, int duration) : base(owner, duration)
        {
            Type = StatusEffectType.Poison;
            _name = "Poison";
        }

        public override void Tick()
        {
            Owner.ApplyDamage(DamageType.Magical, DamagePerTick);
        }
    }

    public class HealOverTimeStatus : StatusEffectOverTime
    {
        public int HealPerTick;

        public HealOverTimeStatus(Character owner, int duration) : base(owner, duration)
        {
            Type = StatusEffectType.HealOverTime;
            _name = "HoT";
        }

        public override void Tick()
        {
            Owner.ApplyHeal(HealPerTick);
        }
    }
}
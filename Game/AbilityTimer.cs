using Microsoft.Xna.Framework;

namespace GameProject
{
    public class AbilityTimer
    {
        protected Character _caster;
        protected Character _target;

        public Character Caster { get => _caster; }
        public Character Target { get => _target; }

        public bool TargettingSelf { get => _caster.Slot == _target.Slot; }

        /// <summary>
        /// In milliseconds
        /// </summary>
        protected int _timeRemaining;
        public int TimeRemaining { get => _timeRemaining; }

        protected Ability _ability = null;
        public Ability Ability { get => _ability; }

        protected bool _finished;
        public bool Finished { get => _finished; }

        public string Name { get => _ability.Name; }

        public AbilityTimer(Character caster, Character target, Ability ability)
        {
            _caster = caster;
            _target = target;
            _ability = ability;

            _timeRemaining = _ability.CastTime;
            _ability.Cast(caster, target);
        }

        public bool Update(GameTime gameTime)
        {
            if (_finished)
                return _finished;

            _timeRemaining -= gameTime.ElapsedGameTime.Milliseconds;

            if (_timeRemaining < 0)
            {
                _finished = true;
                _ability.Apply(_caster, _target);
                _caster.AbilityFinished(this);
            }

            return _finished;
        }

        public void AddTimeRemaining(int time)
        {
            if (_finished)
                return;

            _timeRemaining += time;
        }
    }
}
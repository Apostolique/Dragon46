using Microsoft.Xna.Framework;

namespace GameProject
{
    public class AbilityTimer
    {
        protected Character _caster;
        protected Character _target;

        /// <summary>
        /// In milliseconds
        /// </summary>
        protected int _timeRemaining;
        public int TimeRemaining { get => _timeRemaining; }

        protected Ability _ability = null;

        protected bool _finished;
        public bool Finished { get => _finished; }

        public AbilityTimer(Character caster, Character target, Ability ability)
        {
            _caster = caster;
            _target = target;
            _ability = ability;

            _timeRemaining = _ability.CastTime;
        }

        public bool Update(GameTime gameTime)
        {
            if (_finished)
                return _finished;

            if (_timeRemaining < 0)
            {
                _finished = true;
                _ability.Apply(_caster, _target);
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
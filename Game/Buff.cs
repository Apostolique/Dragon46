using Microsoft.Xna.Framework;

namespace GameProject
{
    public class Buff
    {
        protected Character _owner;
        public Character Owner { get => _owner; }

        /// <summary>
        /// In milliseconds
        /// </summary>
        protected int _timeRemaining;
        public int TimeRemaining { get => _timeRemaining; }

        protected bool _finished;
        public bool Finished { get => _finished; }

        // effects
        public int AddArmour { get; set; }

        public Buff(Character owner, int duration) { _owner = owner; _timeRemaining = duration; }

        public bool Update(GameTime gameTime)
        {
            if (_finished)
                return _finished;

            _timeRemaining -= gameTime.ElapsedGameTime.Milliseconds;

            if (_timeRemaining < 0)
            {
                _finished = true;
                Finish();
            }

            return _finished;
        }

        public void Apply()
        {
            if (AddArmour > 0)
                _owner.AddedArmour += AddArmour;
        }

        public void Finish()
        {
            if (AddArmour > 0)
            {
                _owner.AddedArmour -= AddArmour;
                if (_owner.AddedArmour < 0)
                    _owner.AddedArmour = 0;
            }
        }
    }
}
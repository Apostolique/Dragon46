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
        protected int _totalDuration;

        protected bool _finished;
        public bool Finished { get => _finished; }

        protected string _name;
        public string Name { get => _name; }

        // effects
        public int AddArmour { get; set; }
        public int AddMagicResistance { get; set; }

        public Buff(Character owner, int duration, string name)
        {
            _totalDuration = duration;
            _owner = owner;
            _timeRemaining = duration;
            _name = name;
        }

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

        public void ResetTimer()
        {
            _timeRemaining = _totalDuration;
        }

        public void Apply()
        {
            if (AddArmour > 0)
                _owner.AddedArmour += AddArmour;

            if (AddMagicResistance > 0)
                _owner.AddedMagicResistance += AddMagicResistance;
        }

        public void Finish()
        {
            if (AddArmour > 0)
            {
                _owner.AddedArmour -= AddArmour;
                if (_owner.AddedArmour < 0)
                    _owner.AddedArmour = 0;
            }

            if (AddMagicResistance > 0)
            {
                _owner.AddedMagicResistance -= AddMagicResistance;
                if (_owner.AddedMagicResistance < 0)
                    _owner.AddedMagicResistance = 0;
            }
        }
    }
}
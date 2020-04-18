using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace GameProject
{
    public class SceneManager
    {
        protected Character[] _heroParty;
        protected Character[] _enemyParty;

        // abilities currently casting
        protected List<AbilityTimer> _abilityTimers;

        public SceneManager() { }

        public void Update(GameTime gameTime)
        {
            var removeTimers = new List<AbilityTimer>();

            for (var i = 0; i < _abilityTimers.Count; i++)
                if (_abilityTimers[i].Update(gameTime))
                    removeTimers.Add(_abilityTimers[i]);

            for (var i = 0; i < removeTimers.Count; i++)
                _abilityTimers.Remove(removeTimers[i]);
        }
    }
}
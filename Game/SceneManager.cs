using Microsoft.Xna.Framework;
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
            for (var i = 0; i < _abilityTimers.Count; i++)
                _abilityTimers[i].Update(gameTime);

            _abilityTimers.RemoveAll(t => t.Finished);
        }
    }
}
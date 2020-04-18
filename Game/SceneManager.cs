using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace GameProject
{
    public class SceneManager
    {
        protected Random _rng;

        protected Vector2[] _slotPositions;
        protected Character[] _heroParty;
        protected Character[] _enemyParty;

        // abilities currently casting
        protected List<AbilityTimer> _abilityTimers;

        public SceneManager()
        {
            _rng = new Random();

            _slotPositions = new Vector2[8]
            {
                new Vector2(0, 0), // player
                new Vector2(0, 0), // wizard
                new Vector2(0, 0), // archer
                new Vector2(0, 0), // warrior
                new Vector2(0, 0), // enemy 1
                new Vector2(0, 0), // enemy 2
                new Vector2(0, 0), // enemy 3
                new Vector2(0, 0), // enemy 4
            };
        }

        public void Update(GameTime gameTime)
        {
            for (var i = 0; i < _abilityTimers.Count; i++)
                _abilityTimers[i].Update(gameTime);

            _abilityTimers.RemoveAll(t => t.Finished);

            for (var i = 0; i < _heroParty.Length; i++)
                CheckNextCharacterAbility(_heroParty[i]);
        }

        protected void CheckNextCharacterAbility(Character character)
        {
            if (character == null)
                return;
            if (character.CastingAbility != null && !character.CastingAbility.Finished)
                return;

            var abilities = Database.GetCharacterAbilities(character.Type);
            var nextAbility = abilities[_rng.Next(0, abilities.Count)];

            var target = _heroParty[_rng.Next(0, _heroParty.Length)];
            if (!character.Enemy)
                target = _enemyParty[_rng.Next(0, _enemyParty.Length)];

            var abilityTimer = new AbilityTimer(character, target, nextAbility);
            _abilityTimers.Add(abilityTimer);
            character.CastAbility(abilityTimer);
        }
    }
}
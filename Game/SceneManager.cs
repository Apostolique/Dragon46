using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameProject
{
    public class SceneManager
    {
        protected Random _rng;

        protected Vector2[] _slotPositions;
        protected List<Character> _characters;

        protected List<Character> _heroCharacters
        {
            get { return _characters.Where(c => !c.Enemy).ToList(); }
        }

        protected List<Character> _enemyCharacters
        {
            get { return _characters.Where(c => c.Enemy).ToList(); }
        }

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

            _characters = new List<Character>();
        }

        public void Update(GameTime gameTime)
        {
            for (var i = 0; i < _characters.Count; i++)
            {
                _characters[i].Update(gameTime);
                CheckNextCharacterAbility(_characters[i]);
            }
        }

        protected void CheckNextCharacterAbility(Character character)
        {
            if (character == null)
                return;
            if (character.CastingAbility != null && !character.CastingAbility.Finished)
                return;

            var abilities = Database.GetCharacterAbilities(character.Type);
            var nextAbility = abilities[_rng.Next(0, abilities.Count)];

            var heroes = _heroCharacters;
            var enemies = _enemyCharacters;
            
            var target = heroes[_rng.Next(0, heroes.Count)];
            if (!character.Enemy)
                target = enemies[_rng.Next(0, enemies.Count)];

            var abilityTimer = new AbilityTimer(character, target, nextAbility);
            character.CastAbility(abilityTimer);
        }
    }
}
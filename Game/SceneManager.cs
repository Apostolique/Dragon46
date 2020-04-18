using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameProject
{
    public class SceneManager
    {
        protected int _internalScore;

        protected Random _rng;
        protected GameManager _gameManager;

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

            _gameManager = new GameManager();
            _gameManager.Load(_rng);
        }

        public void Update(GameTime gameTime)
        {
            _internalScore += gameTime.ElapsedGameTime.Milliseconds;

            for (var i = 0; i < _characters.Count; i++)
            {
                _characters[i].Update(gameTime);
                CheckNextCharacterAbility(_characters[i]);
            }

            var heroesAlive = 0;
            foreach (var hero in _heroCharacters)
                if (hero != null && !hero.Dead)
                    heroesAlive += 1;

            if (heroesAlive == 0)
            {
                GameOver();
                return;
            }

            var enemiesAlive = 0;
            foreach (var enemy in _enemyCharacters)
                if (enemy != null && !enemy.Dead)
                    enemiesAlive += 1;

            if (enemiesAlive == 0)
                MoveNextEncounter();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (var i = 0; i < _characters.Count; i++)
            {
                if (!_characters[i].Dead)
                    _characters[i].Draw(spriteBatch, _slotPositions[_characters[i].Slot]);
            }
        }

        protected void MoveNextEncounter()
        {
            var nextEncounter = _gameManager.NextEncounter();

            if (nextEncounter == null)
            {
                // TODO : player won the game
            }

            _characters.RemoveAll(c => c.Enemy);

            var enemySlot = 4;

            for (var i = 0; i < nextEncounter.Enemies.Count; i++)
            {
                var enemyData = nextEncounter.Enemies[i];
                var newEnemy = new Character(enemyData, enemySlot);
                _characters.Add(newEnemy);
                enemySlot++;
            }
        }

        protected void GameOver()
        {
            // TODO : game over
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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameProject
{
    public class SceneManager
    {
        protected GraphicsDevice _graphics;
        protected SpriteBatch _spriteBatch;
        protected int _internalScore;

        protected Random _rng;
        protected GameManager _gameManager;
        protected InGameUIManager _uiManager;

        protected Vector2[] _slotPositions;
        protected List<Character> _characters;

        protected const int _encounterTransitionDuration = 2000;
        protected int _encounterTransitionTimer;
        protected bool _encounterTransition;
        protected bool _gameStarted;

        protected List<Character> _heroCharacters
        {
            get { return _characters.Where(c => !c.Enemy).ToList(); }
        }

        protected List<Character> _enemyCharacters
        {
            get { return _characters.Where(c => c.Enemy).ToList(); }
        }

        public SceneManager(GraphicsDevice graphics)
        {
            _graphics = graphics;
            _spriteBatch = new SpriteBatch(graphics);
            _rng = new Random();

            _slotPositions = new Vector2[8]
            {
                new Vector2(50, 300), // player
                new Vector2(200, 300), // wizard
                new Vector2(350, 300), // archer
                new Vector2(500, 300), // warrior
                new Vector2(900, 300), // enemy 1
                new Vector2(1050, 300), // enemy 2
                new Vector2(1200, 300), // enemy 3
                new Vector2(1350, 300), // enemy 4
            };

            _characters = new List<Character>();

            _characters.Add(new Character(Database.GetHero(CharacterType.Cleric), false, 0, _slotPositions[0], true));
            _characters.Add(new Character(Database.GetHero(CharacterType.Wizard), false, 1, _slotPositions[1]));
            _characters.Add(new Character(Database.GetHero(CharacterType.Archer), false, 2, _slotPositions[2]));
            _characters.Add(new Character(Database.GetHero(CharacterType.Warrior), false, 3, _slotPositions[3]));

            _gameManager = new GameManager();
            _gameManager.Load(_rng);

            _uiManager = new InGameUIManager(_graphics);
        }

        public GameStateType Update(GameTime gameTime)
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

            if (heroesAlive < 4)
            {
                return GameStateType.GameOver;
            }

            var enemiesAlive = 0;
            foreach (var enemy in _enemyCharacters)
                if (enemy != null && !enemy.Dead)
                    enemiesAlive += 1;

            if (enemiesAlive == 0)
            {
                if (_encounterTransition)
                {
                    _encounterTransitionTimer -= gameTime.ElapsedGameTime.Milliseconds;

                    if (_encounterTransitionTimer <= 0)
                    {
                        if (MoveNextEncounter())
                            return GameStateType.PlayerWon;

                        _encounterTransition = false;
                        _gameStarted = true;
                    }
                }
                else
                {
                    CameraWrapper.GoToNextEncounter(_encounterTransitionDuration);
                    _encounterTransitionTimer = _encounterTransitionDuration;
                    _encounterTransition = true;
                }
            }

            _uiManager.Update(gameTime);

            return GameStateType.None;
        }

        public void Draw()
        {
            _spriteBatch.Begin(sortMode: SpriteSortMode.Deferred, blendState: BlendState.AlphaBlend);

            for (var i = 0; i < _characters.Count; i++)
            {
                if (!_characters[i].Dead)
                    _characters[i].Draw(_spriteBatch);
            }

            _uiManager.Draw(_spriteBatch, _characters);

            _spriteBatch.End();
        }

        protected bool MoveNextEncounter()
        {
            var nextEncounter = _gameManager.NextEncounter();

            if (nextEncounter == null)
            {
                return true;
            }

            _characters.RemoveAll(c => c.Enemy);

            var enemySlot = 4;

            for (var i = 0; i < nextEncounter.Enemies.Count; i++)
            {
                var enemy = nextEncounter.Enemies[i];
                var newEnemy = new Character(enemy.EnemyType, true, enemySlot, _slotPositions[enemySlot]);
                _characters.Add(newEnemy);
                enemySlot++;

                Console.WriteLine("New enemy: " + newEnemy.Name);

                if (enemySlot >= _slotPositions.Length)
                    continue;
            }

            return false;
        }

        protected void CheckNextCharacterAbility(Character character)
        {
            if (!_gameStarted)
                return;
            if (_encounterTransition)
                return;
            if (character == null)
                return;
            if (character.IsCasting)
                return;
            if (character.Dead)
                return;
            if (character.CastingCooldown)
                return;
            if (character.Player)
                return;

            var abilities = Database.GetCharacterAbilities(character.Type);
            var nextAbility = abilities[_rng.Next(0, abilities.Count)];

            var heroes = _heroCharacters.Where(c => !c.Dead).ToList();
            var enemies = _enemyCharacters.Where(c => !c.Dead).ToList();

            Character target;
            if (nextAbility.TargetSelf)
            {
                target = character;
            }
            else
            {
                if (!character.Enemy)
                {
                    if (nextAbility.TargetFriendly)
                        target = heroes.Count > 0 ? heroes[_rng.Next(0, enemies.Count)] : null;
                    else
                        target = enemies.Count > 0 ? enemies[_rng.Next(0, enemies.Count)] : null;
                }
                else
                {
                    if (nextAbility.TargetFriendly)
                        target = enemies.Count > 0 ? enemies[_rng.Next(0, enemies.Count)] : null;
                    else
                        target = heroes.Count > 0 ? heroes[_rng.Next(0, enemies.Count)] : null;
                }
            }

            if (target == null)
                return;

            var abilityTimer = new AbilityTimer(character, target, nextAbility);
            character.CastAbility(abilityTimer);
        }
    }
}
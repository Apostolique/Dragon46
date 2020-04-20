using Apos.Input;
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

        public static long InternalScore;

        protected Random _rng;
        protected GameManager _gameManager;
        protected InGameUIManager _uiManager;

        protected Vector2[] _slotPositions;
        protected List<Character> _characters;

        protected const int _encounterTransitionDuration = 2000;
        protected int _encounterTransitionTimer;
        protected bool _encounterTransition;
        protected bool _gameStarted;

        protected int _currentWave;

        protected Ability _playerSelectedAbility;
        protected Character _player;
        protected List<Ability> _playerAbilities;

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
            InternalScore = 0;

            _graphics = graphics;
            _spriteBatch = new SpriteBatch(graphics);
            _rng = new Random();

            _slotPositions = new Vector2[8]
            {
                new Vector2(100, 400), // player
                new Vector2(250, 400), // wizard
                new Vector2(400, 400), // archer
                new Vector2(550, 400), // warrior
                new Vector2(950, 100), // enemy 1
                new Vector2(1100, 200), // enemy 2
                new Vector2(1250, 300), // enemy 3
                new Vector2(1400, 400), // enemy 4
            };

            _characters = new List<Character>();

            _characters.Add(new Character(Database.GetHero(CharacterType.Cleric), false, 0, _slotPositions[0], true));
            _characters.Add(new Character(Database.GetHero(CharacterType.Warrior), false, 3, _slotPositions[3]));
            _characters.Add(new Character(Database.GetHero(CharacterType.Archer), false, 2, _slotPositions[2]));
            _characters.Add(new Character(Database.GetHero(CharacterType.Wizard), false, 1, _slotPositions[1]));

            _player = _characters[0];
            _playerAbilities = Database.GetCharacterAbilities(CharacterType.Cleric);

            _gameManager = new GameManager();
            _gameManager.Load(_rng);

            _uiManager = new InGameUIManager(_graphics);
        }

        public GameStateType Update(GameTime gameTime)
        {
            InternalScore += gameTime.ElapsedGameTime.Milliseconds;

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

            CheckInput();

            return GameStateType.None;
        }

        public void CheckInput()
        {
            if (Triggers.PlayerSkill1.Released())
            {
                _uiManager.PlayerSelectedAbility = 1;
                _playerSelectedAbility = _playerAbilities[0];
            }
            else if (Triggers.PlayerSkill2.Released())
            {
                _uiManager.PlayerSelectedAbility = 2;
                _playerSelectedAbility = _playerAbilities[1];
            }
            else if (Triggers.PlayerSkill3.Released())
            {
                _uiManager.PlayerSelectedAbility = 3;
                _playerSelectedAbility = _playerAbilities[2];
            }
            else if (Triggers.PlayerSkill4.Released())
            {
                _uiManager.PlayerSelectedAbility = 4;
                _playerSelectedAbility = _playerAbilities[3];
            }
            else if (Triggers.PlayerSkill5.Released())
            {
                _uiManager.PlayerSelectedAbility = 5;
                _playerSelectedAbility = _playerAbilities[4];
            }
            else if (Triggers.PlayerSkill6.Released())
            {
                _uiManager.PlayerSelectedAbility = 6;
                _playerSelectedAbility = _playerAbilities[5];
            }

            if (!_player.IsCasting && !_encounterTransition)
            {
                if (_playerSelectedAbility != null)
                {
                    if (Triggers.MouseLeftClick.Released())
                    {
                        var mousePosition = new Vector2(InputHelper.NewMouse.X, InputHelper.NewMouse.Y);

                        Character clickedCharacter = null;

                        for (var i = 0; i < _characters.Count; i++)
                            if (_characters[i].PointInCharacter(mousePosition))
                                clickedCharacter = _characters[i];

                        if (clickedCharacter != null)
                        {
                            if (_playerSelectedAbility.TargetFriendly && clickedCharacter.Enemy)
                                return;
                            if (!_playerSelectedAbility.TargetFriendly && !clickedCharacter.Enemy)
                                return;

                            var abilityTimer = new AbilityTimer(_player, clickedCharacter, _playerSelectedAbility);
                            _player.CastAbility(abilityTimer);
                            _uiManager.PlayerSelectedAbility = -1;
                            _playerSelectedAbility = null;
                        }
                    }
                }
            }
        }

        public void DrawGame()
        {
            _spriteBatch.Begin(sortMode: SpriteSortMode.Deferred, blendState: BlendState.AlphaBlend, samplerState: SamplerState.PointClamp);

            for (var i = 0; i < _characters.Count; i++)
            {
                if (!_characters[i].Dead)
                    _characters[i].Draw(_spriteBatch);
            }

            _spriteBatch.End();
        }

        public void DrawUI()
        {
            _spriteBatch.Begin(sortMode: SpriteSortMode.Deferred, blendState: BlendState.AlphaBlend, samplerState: SamplerState.PointClamp);

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

                if (enemySlot >= _slotPositions.Length)
                    continue;
            }

            _currentWave += 1;
            _uiManager.CurrentWave = _currentWave;

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
                        target = heroes.Count > 0 ? heroes[_rng.Next(0, heroes.Count)] : null;
                    else
                        target = enemies.Count > 0 ? enemies[_rng.Next(0, enemies.Count)] : null;
                }
                else
                {
                    if (nextAbility.TargetFriendly)
                        target = enemies.Count > 0 ? enemies[_rng.Next(0, enemies.Count)] : null;
                    else
                        target = heroes.Count > 0 ? heroes[_rng.Next(0, heroes.Count)] : null;
                }
            }

            if (target == null)
                return;

            var abilityTimer = new AbilityTimer(character, target, nextAbility);
            character.CastAbility(abilityTimer);
        }
    }
}
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameProject
{
    public class InGameState : GameState
    {
        protected SpriteBatch _spriteBatch;
        protected SceneManager _sceneManager;
        protected Backgrounds _backgrounds;
        protected Foregrounds _foregrounds;

        protected int _gameMusic = -1;

        public InGameState(GraphicsDevice graphics) : base(graphics)
        {
            _spriteBatch = new SpriteBatch(graphics);
            _sceneManager = new SceneManager(graphics);

            _backgrounds = new Backgrounds(graphics);
            _foregrounds = new Foregrounds(graphics);

            _gameMusic = Assets.SoundManager.PlaySound("music_test", (int)SoundType.Music, true);
        }

        public override GameStateType Update(GameTime gameTime)
        {
            Core.Update(gameTime);

            var newState = _sceneManager.Update(gameTime);

            _backgrounds.Update(gameTime);
            _foregrounds.Update(gameTime);

            if (newState != GameStateType.None)
                Assets.SoundManager.StopByID(_gameMusic);

            return newState;
        }

        public override void Draw()
        {
            _backgrounds.Draw();
            _sceneManager.DrawGame();
            _foregrounds.Draw();
            _sceneManager.DrawUI();
        }
    }
}
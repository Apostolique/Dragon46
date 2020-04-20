using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameProject
{
    public class InGameState : GameState
    {
        protected SpriteBatch _spriteBatch;
        protected SceneManager _sceneManager;
        public InGameState(GraphicsDevice graphics) : base(graphics)
        {
            _spriteBatch = new SpriteBatch(graphics);
            _sceneManager = new SceneManager(graphics);

            UIHelper.Clear();
            // add buttons
        }

        public override GameStateType Update(GameTime gameTime)
        {
            Core.Update(gameTime);

            var newState = _sceneManager.Update(gameTime);

            GameRoot.Instance.Backgrounds.Update(gameTime);
            GameRoot.Instance.Foregrounds.Update(gameTime);

            return newState;
        }

        public override void Draw()
        {
            GameRoot.Instance.Backgrounds.Draw();
            _sceneManager.DrawGame();
            GameRoot.Instance.Foregrounds.Draw();
            _sceneManager.DrawUI();
        }
    }
}
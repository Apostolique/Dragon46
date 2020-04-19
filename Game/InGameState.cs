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

        public InGameState(GraphicsDevice graphics) : base(graphics)
        {
            _spriteBatch = new SpriteBatch(graphics);
            _sceneManager = new SceneManager(graphics);

            _backgrounds = new Backgrounds(graphics);
        }

        public override GameStateType Update(GameTime gameTime)
        {
            Core.Update(gameTime);

            var newState = _sceneManager.Update(gameTime);

            _backgrounds.Update(gameTime);

            return newState;
        }

        public override void Draw()
        {
            _backgrounds.Draw();
            _sceneManager.Draw();
            //TODO : draw scene manager
        }
    }
}
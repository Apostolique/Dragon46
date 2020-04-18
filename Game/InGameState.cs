using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameProject
{
    public class InGameState : GameState
    {
        protected SceneManager _sceneManager;

        public InGameState(GraphicsDevice graphics) : base(graphics)
        {
            _sceneManager = new SceneManager();
        }
        
        public override GameStateType Update(GameTime gameTime)
        {
            _sceneManager.Update(gameTime);
            return GameStateType.None;
        }

        public override void Draw()
        {
            throw new NotImplementedException("Draw _sceneManager");
        }
    }
}
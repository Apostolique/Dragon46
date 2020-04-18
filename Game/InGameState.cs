using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
            return GameStateType.None;
        }

        public override void Draw()
        {
            
        }
    }
}
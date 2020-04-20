using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject
{
    public class GameOverState : GameState
    {
        public GameOverState(GraphicsDevice graphics) : base(graphics)
        {
            Assets.SoundManager.StopAll();
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
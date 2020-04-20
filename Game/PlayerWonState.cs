using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject
{
    public class PlayerWonState : GameState
    {
        public PlayerWonState(GraphicsDevice graphics) : base(graphics) { }

        public override GameStateType Update(GameTime gameTime)
        {
            return GameStateType.None;
        }

        public override void Draw()
        {

        }
    }
}
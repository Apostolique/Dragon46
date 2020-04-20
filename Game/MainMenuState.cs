using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject
{
    public class MainMenuState : GameState
    {
        public MainMenuState(GraphicsDevice graphics) : base(graphics) { }

        public override GameStateType Update(GameTime gameTime)
        {
            return GameStateType.None;
        }

        public override void Draw()
        {

        }
    }
}
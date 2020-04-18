using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject
{
    public enum GameStateType
    {
        None, MainMenu, InGame
    }

    public class GameState
    {
        protected GraphicsDevice _graphics;

        public GameState(GraphicsDevice graphics) { _graphics = graphics; }

        public virtual GameStateType Update(GameTime gameTime) { return GameStateType.None; }
        public virtual void Draw() { }
    }
}
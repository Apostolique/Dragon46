using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpriteFontPlus;
using System.Collections.Generic;

namespace GameProject
{
    public class ScreenText
    {
        public string Text;
        public Color Colour;
        public int Duration;
        public bool Finished;
        public Vector2 Position;
        public Vector2 Velocity;
        public int TextSize = 20;
    }

    public static class ScreenEffectsManager
    {
        private static List<ScreenText> _screenText = new List<ScreenText>();

        public static void AddScreenText(ScreenText screenText)
        {
            _screenText.Add(screenText);
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            for (var i = 0; i < _screenText.Count; i++)
            {
                var screenText = _screenText[i];
                Assets.UIFont.Size = screenText.TextSize;
                spriteBatch.DrawString(Assets.UIFont, screenText.Text, screenText.Position, screenText.Colour);
            }
        }

        public static void Update(GameTime gameTime)
        {
            var delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            for (var i = 0; i < _screenText.Count; i++)
            {
                var screenText = _screenText[i];
                screenText.Position += (screenText.Velocity * delta);
                screenText.Duration -= gameTime.ElapsedGameTime.Milliseconds;

                if (screenText.Duration <= 0)
                    screenText.Finished = true;
            }

            _screenText.RemoveAll(t => t.Finished);
        }
    }
}
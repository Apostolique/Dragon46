using Apos.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpriteFontPlus;
using System;
using System.Collections.Generic;

namespace GameProject
{
    public class UIElement
    {
        public Vector2 Position = Vector2.Zero;
        public int Width { get; set; }
        public int Height { get; set; }
        public int Size { get; set; }
        public int Padding { get; set; }

        public void CenterX(GraphicsDevice graphics)
        {
            Position.X = (graphics.PresentationParameters.BackBufferWidth / 2) - (Width / 2);
        }

        public void CenterY(GraphicsDevice graphics)
        {
            Position.Y = (graphics.PresentationParameters.BackBufferHeight / 2) - (Height / 2);
        }

        public void PinLeft(GraphicsDevice graphics, int offset = 0)
        {
            Position.X = 0 + offset;
        }

        public void PinRight(GraphicsDevice graphics, int offset = 0)
        {
            Position.X = graphics.PresentationParameters.BackBufferWidth - Width - offset;
        }

        public void PinTop(GraphicsDevice graphics, int offset = 0)
        {
            Position.Y = 0 + offset;
        }

        public void PinBottom(GraphicsDevice graphics, int offset = 0)
        {
            Position.Y = graphics.PresentationParameters.BackBufferHeight - Height - offset;
        }
    }

    public class UILabel : UIElement
    {
        public string Text { get; set; }
        public Color colour { get; set; } = Color.White;

        public UILabel(string text, int size = 20, int padding = 5)
        {
            UpdateLabel(text, size, padding);
        }

        public void UpdateLabel(string text, int size = 20, int padding = 5)
        {
            Size = size;
            Padding = padding;
            Assets.UIFont.Size = Size;
            Text = text;
            var textSize = Assets.UIFont.MeasureString(text);
            Width = (int)textSize.X + (padding * 2);
            Height = (int)textSize.Y + (padding * 2);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            UIHelper.DrawText(spriteBatch, Text, Position, colour, Size, Padding);
        }
    }

    public class UIButton : UIElement
    {
        public string Text { get; set; }
        public Color colour { get; set; } = Color.White;
        public Color hoverColour { get; set; } = Color.Black;

        public Action OnClick { get; set; } = null;

        public UIButton(string text, int size, int padding)
        {
            Size = size;
            Padding = padding;
            Assets.UIFont.Size = Size;
            Text = text;
            var textSize = Assets.UIFont.MeasureString(text);
            Width = (int)textSize.X + (padding * 2);
            Height = (int)textSize.Y + (padding * 2);
        }

        public bool Draw(SpriteBatch spriteBatch, Vector2 mousePosition)
        {
            var hover = PointInButton(mousePosition);

            UIHelper.DrawText(spriteBatch, Text, Position, hover ? hoverColour : colour, Size, Padding);

            if (Triggers.MouseLeftClick.Released() && hover)
            {
                OnClick?.Invoke();
                return true;
            }

            return false;
        }

        public bool PointInButton(Vector2 point)
        {
            var buttonRect = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);

            if (point.X < buttonRect.X)
                return false;
            if (point.Y < buttonRect.Y)
                return false;
            if (point.X > (buttonRect.X + buttonRect.Width))
                return false;
            if (point.Y > (buttonRect.Y + buttonRect.Height))
                return false;

            return true;
        }
    }

    public static class UIHelper
    {
        private static GraphicsDevice _graphics;
        private static Texture2D _textBackground;

        private static List<UIButton> _buttons = new List<UIButton>();
        private static List<UILabel> _labels = new List<UILabel>();

        public static void Setup(GraphicsDevice graphics)
        {
            _graphics = graphics;

            var textBGColour = new Color(0, 0, 0, 120);

            var tempBG = new RenderTarget2D(graphics, graphics.PresentationParameters.BackBufferWidth, graphics.PresentationParameters.BackBufferHeight);
            graphics.SetRenderTarget(tempBG);
            graphics.Clear(textBGColour);
            graphics.SetRenderTarget(null);
            _textBackground = (Texture2D)tempBG;
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            var mousePosition = new Vector2(InputHelper.NewMouse.X, InputHelper.NewMouse.Y);

            for (var i = 0; i < _buttons.Count; i++)
                _buttons[i]?.Draw(spriteBatch, mousePosition);

            for (var i = 0; i < _labels.Count; i++)
                _labels[i]?.Draw(spriteBatch);
        }

        public static void AddLabel(UILabel label)
        {
            _labels.Add(label);
        }
        public static void AddButton(UIButton button)
        {
            _buttons.Add(button);
        }

        public static void Clear()
        {
            _buttons.Clear();
            _labels.Clear();
        }

        public static void Cleanup()
        {
            _textBackground?.Dispose();
        }

        public static void DrawText(SpriteBatch spriteBatch, string text, Vector2 position, Color colour, int size = 20, int padding = 3)
        {
            if (text.Length == 0)
                return;

            Assets.UIFont.Size = size;

            var textSize = Assets.UIFont.MeasureString(text);

            var textBGPosition = position - new Vector2(padding, padding);
            var textBGRect = new Rectangle((int)textBGPosition.X, (int)textBGPosition.Y, (int)textSize.X + (padding * 2), (int)textSize.Y + (padding * 2));

            spriteBatch.Draw(_textBackground, textBGPosition, textBGRect, Color.White);
            spriteBatch.DrawString(Assets.UIFont, text, position, colour);
        }
    }
}
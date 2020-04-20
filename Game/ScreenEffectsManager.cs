using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpriteFontPlus;
using System.Collections.Generic;
using System.Linq;

namespace GameProject
{
    public enum FadeDirection
    {
        None = 0,
        FadeIn = 1,
        FadeOut = -1
    };

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

    public class CharacterFadeEffect
    {
        public Character Character;
        public bool Finished;
        public int Duration;
        public float TargetTransparency;

        protected FadeDirection _fadeDirection = FadeDirection.None;
        protected float _fadeTransparency = 0.0f;
        protected float _fadeChangePerSecond = 0.0f;

        protected bool _firstStageFinished;

        protected Color _startColour;
        public Color Colour { get => Character.Sprite.Colour; }

        public void Start(bool secondStage = false)
        {
            if ((byte)TargetTransparency == Colour.A)
                return;

            _fadeDirection = FadeDirection.FadeIn;

            if (TargetTransparency < (float)Colour.A)
                _fadeDirection = FadeDirection.FadeOut;

            float totalFadeChange = TargetTransparency - Colour.A;
            if (totalFadeChange < 0)
                totalFadeChange *= -1;

            _fadeChangePerSecond = totalFadeChange / (Duration / 1000.0f);

            if (!secondStage)
            {
                _startColour = Character.Sprite.Colour;
                Character.Sprite.Colour = Color.Red;
            }
        }

        public void Update(GameTime gameTime)
        {
            if (Finished)
                return;
            if (_fadeDirection == FadeDirection.None)
                return;

            _fadeTransparency += (_fadeChangePerSecond * ((float)gameTime.ElapsedGameTime.Milliseconds / 1000.0f)) * (float)_fadeDirection;

            var stageFinished = false;

            if (_fadeDirection == FadeDirection.FadeIn && _fadeTransparency >= TargetTransparency)
            {
                _fadeTransparency = TargetTransparency;
                _fadeDirection = FadeDirection.None;
                stageFinished = true;
            }
            else if (_fadeDirection == FadeDirection.FadeOut && _fadeTransparency <= TargetTransparency)
            {
                _fadeTransparency = TargetTransparency;
                _fadeDirection = FadeDirection.None;
                stageFinished = true;
            }

            Character.Sprite.Colour.A = (byte)_fadeTransparency;

            if (stageFinished)
            {
                if (!_firstStageFinished)
                {
                    TargetTransparency = 255;
                    _firstStageFinished = true;
                    Start(true);
                }
                else
                {
                    Finished = true;
                    _fadeDirection = FadeDirection.None;
                    Character.Sprite.Colour = _startColour;
                }
            }
        }
    }

    public static class ScreenEffectsManager
    {
        private static List<ScreenText> _screenText = new List<ScreenText>();
        private static List<CharacterFadeEffect> _characterFadeEffects = new List<CharacterFadeEffect>();

        public static void AddScreenText(ScreenText screenText)
        {
            _screenText.Add(screenText);
        }

        public static void AddCharacterFadeEffect(CharacterFadeEffect fadeEffect)
        {
            if (_characterFadeEffects.Where(e => e.Character.Name == fadeEffect.Character.Name).Count() > 0)
                return;

            fadeEffect.Start();
            _characterFadeEffects.Add(fadeEffect);
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

            for (var i = 0; i < _characterFadeEffects.Count; i++)
                _characterFadeEffects[i]?.Update(gameTime);

            _screenText.RemoveAll(t => t.Finished);
            _characterFadeEffects.RemoveAll(e => e.Finished);
        }
    }
}
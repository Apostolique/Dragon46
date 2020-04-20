using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpriteFontPlus;
using System.Collections.Generic;

namespace GameProject
{
    public class InGameUIManager
    {
        protected GraphicsDevice _graphics;
        public int CurrentWave;

        public List<Ability> PlayerAbilities;
        public int PlayerSelectedAbility = -1;

        protected Texture2D _textBackground;

        public InGameUIManager(GraphicsDevice graphics)
        {
            _graphics = graphics;

            PlayerAbilities = Database.GetCharacterAbilities(CharacterType.Cleric);

            var textBGColour = new Color(0, 0, 0, 80);

            var tempBG = new RenderTarget2D(graphics, graphics.PresentationParameters.BackBufferWidth, graphics.PresentationParameters.BackBufferHeight);
            graphics.SetRenderTarget(tempBG);
            graphics.Clear(textBGColour);
            graphics.SetRenderTarget(null);
            _textBackground = (Texture2D)tempBG;
        }

        ~InGameUIManager()
        {
            _textBackground?.Dispose();
        }

        public void UpdateUISize()
        {

        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch, List<Character> characters)
        {
            var playerSkillbarPosition = new Vector2(50, 50);

            if (CurrentWave > 0)
            {
                var waveString = "Wave #" + CurrentWave;

                Assets.UIFont.Size = 42;
                var waveSize = Assets.UIFont.MeasureString(waveString);
                var wavePosition = new Vector2((_graphics.PresentationParameters.BackBufferWidth / 2) - (waveSize.X / 2), 50);

                DrawText(spriteBatch, waveString, wavePosition, Color.White, 42);
            }

            for (var i = 0; i < PlayerAbilities.Count; i++)
            {
                var ability = PlayerAbilities[i];
                var color = Color.White;

                if (PlayerSelectedAbility > -1)
                {
                    if (PlayerSelectedAbility == (i + 1))
                        color = Color.Black;
                }

                var skillString = "(" + (i + 1) + ") " + ability.Name + " - " + ((float)ability.CastTime / 1000).ToString("0.0") + "s";
                DrawText(spriteBatch, skillString, playerSkillbarPosition, color);

                playerSkillbarPosition.Y += 30;
            }

            for (var i = 0; i < characters.Count; i++)
            {
                var character = characters[i];
                if (character.Dead)
                    continue;

                var namePosition = character.DrawPosition;
                namePosition.Y -= 75;

                DrawText(spriteBatch, character.Name, namePosition, Color.White);

                var hpPosition = namePosition;
                hpPosition.Y += 25;

                var hpColor = Color.White;
                if (character.CurrentHP <= (character.MaxHP / 2))
                    hpColor = Color.Red;

                DrawText(spriteBatch, character.CurrentHP + "/" + character.MaxHP, hpPosition, hpColor);

                var statusPosition = hpPosition;
                statusPosition.Y += 25;

                var statusString = "";

                for (var s = 0; s < character.StatusEffects.Count; s++)
                    statusString += character.StatusEffects[s].Name + ", ";

                for (var b = 0; b < character.Buffs.Count; b++)
                    statusString += character.Buffs[b].Name + ", ";

                if (statusString.Length > 0)
                {
                    statusString = statusString.Remove(statusString.Length - 2);
                    DrawText(spriteBatch, statusString, statusPosition, Color.White);
                }

                var skillPosition = statusPosition;
                skillPosition.Y += 25;
                
                var skillString = "";
                if (character.IsCasting)
                    skillString = character.CastingAbility.Name + " " + ((float)character.CastingAbility.TimeRemaining / 1000).ToString("0.0") + "s";

                DrawText(spriteBatch, skillString, skillPosition, Color.White);
            }
        }

        public void DrawText(SpriteBatch spriteBatch, string text, Vector2 position, Color colour, int size = 20)
        {
            if (text.Length == 0)
                return;

            Assets.UIFont.Size = size;
            var padding = 3;

            var textSize = Assets.UIFont.MeasureString(text);

            var textBGPosition = position - new Vector2(padding, padding);
            var textBGRect = new Rectangle((int)textBGPosition.X, (int)textBGPosition.Y, (int)textSize.X + (padding * 2), (int)textSize.Y + (padding * 2));

            spriteBatch.Draw(_textBackground, textBGPosition, textBGRect, Color.White);
            spriteBatch.DrawString(Assets.UIFont, text, position, colour);
        }
    }
}
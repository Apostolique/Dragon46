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

        public InGameUIManager(GraphicsDevice graphics)
        {
            _graphics = graphics;

            PlayerAbilities = Database.GetCharacterAbilities(CharacterType.Cleric);
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
            var heroSkillPosition = new Vector2(100, 725);
            var enemySkillPosition = new Vector2(950, 725);

            if (CurrentWave > 0)
            {
                var waveString = "Wave #" + CurrentWave;

                Assets.UIFont.Size = 42;
                var waveSize = Assets.UIFont.MeasureString(waveString);
                var wavePosition = new Vector2((_graphics.PresentationParameters.BackBufferWidth / 2) - (waveSize.X / 2), 50);

                spriteBatch.DrawString(Assets.UIFont, waveString, wavePosition, Color.White);
            }

            Assets.UIFont.Size = 20;

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
                spriteBatch.DrawString(Assets.UIFont, skillString, playerSkillbarPosition, color);

                playerSkillbarPosition.Y += 25;
            }

            for (var i = 0; i < characters.Count; i++)
            {
                var character = characters[i];
                if (character.Dead)
                    continue;

                var namePosition = character.DrawPosition;
                namePosition.Y -= 75;

                spriteBatch.DrawString(Assets.UIFont, "#" + character.Slot + " " + character.Name, namePosition, Color.White);

                var hpPosition = namePosition;
                hpPosition.Y += 20;

                spriteBatch.DrawString(Assets.UIFont, character.CurrentHP + "/" + character.MaxHP, hpPosition, Color.White);

                var statusPosition = hpPosition;
                statusPosition.Y += 20;

                var statusString = "";

                for (var s = 0; s < character.StatusEffects.Count; s++)
                    statusString += character.StatusEffects[s].Name + ", ";

                for (var b = 0; b < character.Buffs.Count; b++)
                    statusString += character.Buffs[b].Name + ", ";

                if (statusString.Length > 0)
                {
                    statusString = statusString.Remove(statusString.Length - 2);
                    spriteBatch.DrawString(Assets.UIFont, statusString, statusPosition, Color.White);
                }

                var skillPosition = statusPosition;
                skillPosition.Y += 20;
                
                var skillString = "";
                if (character.IsCasting)
                    skillString = character.CastingAbility.Name + " " + ((float)character.CastingAbility.TimeRemaining / 1000).ToString("0.0") + "s";

                spriteBatch.DrawString(Assets.UIFont, skillString, skillPosition, Color.White);
            }
        }
    }
}
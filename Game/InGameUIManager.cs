using Apos.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        public void Draw(ShapeBatch shapeBatch, List<Character> characters)
        {
            var playerSkillbarPosition = new Vector2(50, 50);

            if (CurrentWave > 0)
            {
                var waveString = "Wave #" + CurrentWave;

                var waveSize = UIHelper.MeasureText(waveString, 42);
                var wavePosition = new Vector2((_graphics.PresentationParameters.BackBufferWidth / 2) - (waveSize.X / 2), 50);

                UIHelper.DrawText(shapeBatch, waveString, wavePosition, Color.White, 42);
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
                UIHelper.DrawText(shapeBatch, skillString, playerSkillbarPosition, color);

                playerSkillbarPosition.Y += 30;
            }

            for (var i = 0; i < characters.Count; i++)
            {
                var character = characters[i];
                if (character.Dead)
                    continue;

                var namePosition = character.DrawPosition;
                namePosition.Y -= 75;

                UIHelper.DrawText(shapeBatch, character.Name, namePosition, character.Hover ? Color.Yellow : Color.White);

                var hpPosition = namePosition;
                hpPosition.Y += 25;

                var hpColor = Color.White;
                if (character.CurrentHP <= (character.MaxHP / 2))
                    hpColor = Color.Red;

                UIHelper.DrawText(shapeBatch, character.CurrentHP + "/" + character.MaxHP, hpPosition, hpColor);

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
                    UIHelper.DrawText(shapeBatch, statusString, statusPosition, Color.White);
                }

                var skillPosition = statusPosition;
                skillPosition.Y += 25;
                
                var skillString = "";
                if (character.IsCasting)
                    skillString = character.CastingAbility.Name + " " + ((float)character.CastingAbility.TimeRemaining / 1000).ToString("0.0") + "s";

                UIHelper.DrawText(shapeBatch, skillString, skillPosition, Color.White);
            }
        }
    }
}
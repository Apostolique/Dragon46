using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpriteFontPlus;
using System.Collections.Generic;

namespace GameProject
{
    public class InGameUIManager
    {
        protected GraphicsDevice _graphics;

        public InGameUIManager(GraphicsDevice graphics)
        {
            _graphics = graphics;
        }

        public void UpdateUISize()
        {

        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch, List<Character> characters)
        {
            var heroSkillPosition = new Vector2(50, 625);
            var enemySkillPosition = new Vector2(900, 625);

            Assets.UIFont.Size = 20;

            for (var i = 0; i < characters.Count; i++)
            {
                var character = characters[i];
                if (character.Dead)
                    continue;

                var namePosition = character.DrawPosition;
                namePosition.Y -= 50;

                spriteBatch.DrawString(Assets.UIFont, character.Name, namePosition, Color.White);

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

                Vector2 skillPosition;
                if (!character.Enemy)
                {
                    skillPosition = heroSkillPosition;
                    heroSkillPosition.Y += 35;
                }
                else
                {
                    skillPosition = enemySkillPosition;
                    enemySkillPosition.Y += 35;
                }

                var skillString = "";
                if (character.IsCasting)
                    skillString = character.CastingAbility.Name + " " + ((float)character.CastingAbility.TimeRemaining / 1000).ToString("0.0") + "s";

                spriteBatch.DrawString(Assets.UIFont, character.Name + ": " + skillString, skillPosition, Color.White);
            }
        }
    }
}
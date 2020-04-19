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
            var heroSkillPosition = new Vector2(50, 650);
            var enemySkillPosition = new Vector2(900, 650);

            Assets.UIFont.Size = 20;

            for (var i = 0; i < characters.Count; i++)
            {
                var character = characters[i];
                var namePosition = character.DrawPosition;
                namePosition.Y -= 25;

                spriteBatch.DrawString(Assets.UIFont, character.Name, namePosition, Color.White);

                var hpPosition = namePosition;
                hpPosition.Y += 20;

                spriteBatch.DrawString(Assets.UIFont, character.CurrentHP + "/" + character.MaxHP, hpPosition, Color.White);

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
                    skillString = character.CastingAbility.Name + " " + character.CastingAbility.TimeRemaining;

                spriteBatch.DrawString(Assets.UIFont, character.Name + ": " + skillString, skillPosition, Color.White);
            }
        }
    }
}
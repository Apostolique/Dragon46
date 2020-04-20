using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject
{
    public class CreditsState : GameState
    {
        protected GameStateType _newState = GameStateType.None;

        public CreditsState(GraphicsDevice graphics) : base(graphics)
        {
            UIHelper.Clear();

            var titleLabel = new UILabel("Credits", 60, 5);
            titleLabel.PinTop(graphics, 50);
            titleLabel.CenterX(graphics);

            var backButton = new UIButton("> Back to Menu", 42, 5)
            {
                OnClick = () => { _newState = GameStateType.MainMenu; }
            };
            backButton.CenterX(graphics);
            backButton.Position.Y = 500;

            var helpLabel1 = new UILabel("Design and develoment by Jesse Gill (@PandepicGames)", 42, 5);
            helpLabel1.CenterX(graphics);
            helpLabel1.Position.Y = 250;

            var helpLabel2 = new UILabel("Design and develoment by Jean-David (@JeanDavidMoisan)", 42, 5);
            helpLabel2.CenterX(graphics);
            helpLabel2.Position.Y = 305;

            var helpLabel3 = new UILabel("Art by Sarah Plowman", 42, 5);
            helpLabel3.CenterX(graphics);
            helpLabel3.Position.Y = 360;

            var helpLabel4 = new UILabel("Music by Eric Matyas (www.soundimage.org)", 42, 5);
            helpLabel4.CenterX(graphics);
            helpLabel4.Position.Y = 415;

            UIHelper.AddButton(backButton);
            UIHelper.AddLabel(titleLabel);
            UIHelper.AddLabel(helpLabel1);
            UIHelper.AddLabel(helpLabel2);
            UIHelper.AddLabel(helpLabel3);
            UIHelper.AddLabel(helpLabel4);
        }

        public override GameStateType Update(GameTime gameTime)
        {
            GameRoot.Instance.Backgrounds.Update(gameTime);
            GameRoot.Instance.Foregrounds.Update(gameTime);

            return _newState;
        }

        public override void Draw()
        {
            GameRoot.Instance.Backgrounds.Draw();
            GameRoot.Instance.Foregrounds.Draw();
        }
    }
}
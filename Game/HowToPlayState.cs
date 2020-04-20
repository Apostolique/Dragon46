using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject
{
    public class HowToPlayState : GameState
    {
        protected SpriteBatch _spriteBatch;

        protected GameStateType _newState = GameStateType.None;

        public HowToPlayState(GraphicsDevice graphics) : base(graphics)
        {
            _spriteBatch = new SpriteBatch(graphics);

            UIHelper.Clear();

            var titleLabel = new UILabel("How to Play", 60, 5);
            titleLabel.PinTop(graphics, 50);
            titleLabel.CenterX(graphics);

            var helpLabel1 = new UILabel("Travel through the forest and keep your party alive.", 42, 5);
            helpLabel1.CenterX(graphics);
            helpLabel1.Position.Y = 250;

            var helpLabel2 = new UILabel("Use the number keys 1-5 to select a spell and then click on your target.", 42, 5);
            helpLabel2.CenterX(graphics);
            helpLabel2.Position.Y = 305;

            var helpLabel3 = new UILabel("If any of your party die you will lose the game.", 42, 5);
            helpLabel3.CenterX(graphics);
            helpLabel3.Position.Y = 360;

            var startButton = new UIButton("Start Game", 42, 5)
            {
                OnClick = () => { _newState = GameStateType.InGame; }
            };
            startButton.CenterX(graphics);
            startButton.Position.Y = 450;

            UIHelper.AddButton(startButton);
            UIHelper.AddLabel(titleLabel);
            UIHelper.AddLabel(helpLabel1);
            UIHelper.AddLabel(helpLabel2);
            UIHelper.AddLabel(helpLabel3);
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
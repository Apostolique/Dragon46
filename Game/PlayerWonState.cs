using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject
{
    public class PlayerWonState : GameState
    {
        protected GameStateType _newState = GameStateType.None;

        public PlayerWonState(GraphicsDevice graphics) : base(graphics)
        {
            //Assets.SoundManager.StopAll();

            UIHelper.Clear();

            var titleLabel = new UILabel("You Won!", 60, 5);
            titleLabel.PinTop(graphics, 50);
            titleLabel.CenterX(graphics);

            var startButton = new UIButton("> Back to Menu", 42, 5)
            {
                OnClick = () => { _newState = GameStateType.MainMenu; }
            };
            startButton.CenterX(graphics);
            startButton.Position.Y = 350;

            var exitButton = new UIButton("> Exit Game", 42, 5)
            {
                OnClick = () => { GameRoot.Instance.ExitGame(); }
            };
            exitButton.CenterX(graphics);
            exitButton.Position.Y = 425;

            UIHelper.AddButton(startButton);
            UIHelper.AddButton(exitButton);
            UIHelper.AddLabel(titleLabel);
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
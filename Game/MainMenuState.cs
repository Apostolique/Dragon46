using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject
{
    public class MainMenuState : GameState
    {
        protected SpriteBatch _spriteBatch;
        protected GameStateType _newState = GameStateType.None;

        public MainMenuState(GraphicsDevice graphics) : base(graphics)
        {
            _spriteBatch = new SpriteBatch(graphics);

            Assets.SoundManager.PlaySound("music_test", (int)SoundType.Music, true);

            UIHelper.Clear();

            var titleLabel = new UILabel("Looking for Group", 60, 5);
            titleLabel.PinTop(graphics, 50);
            titleLabel.CenterX(graphics);

            var startButton = new UIButton("> New Game", 42, 5)
            {
                OnClick = () => { _newState = GameStateType.HowToPlay; }
            };
            startButton.CenterX(graphics);
            startButton.Position.Y = 350;

            var creditsButton = new UIButton("> Credits", 42, 5)
            {
                OnClick = () => { _newState = GameStateType.Credits; }
            };
            creditsButton.CenterX(graphics);
            creditsButton.Position.Y = 425;

            var exitButton = new UIButton("< Exit Game", 42, 5)
            {
                OnClick = () => { GameRoot.Instance.ExitGame(); }
            };
            exitButton.CenterX(graphics);
            exitButton.Position.Y = 500;

            UIHelper.AddButton(startButton);
            UIHelper.AddButton(creditsButton);
            UIHelper.AddButton(exitButton);
            UIHelper.AddLabel(titleLabel);
        }

        public override GameStateType Update(GameTime gameTime)
        {
            Core.Update(gameTime);

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
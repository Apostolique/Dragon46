using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameProject
{
    public class InGameState : GameState
    {
        protected SpriteBatch _spriteBatch;
        protected SceneManager _sceneManager;

        protected GameStateType _newState = GameStateType.None;

        public InGameState(GraphicsDevice graphics) : base(graphics)
        {
            _spriteBatch = new SpriteBatch(graphics);
            _sceneManager = new SceneManager(graphics);

            UIHelper.Clear();

            var startButton = new UIButton("> Back to Menu", 32, 5)
            {
                OnClick = () => { _newState = GameStateType.MainMenu; }
            };
            startButton.CenterX(graphics);
            startButton.PinTop(graphics, 50);
            startButton.PinRight(graphics, 50);

            UIHelper.AddButton(startButton);
        }

        public override GameStateType Update(GameTime gameTime)
        {
            Core.Update(gameTime);

            var sceneState = _sceneManager.Update(gameTime);

            GameRoot.Instance.Backgrounds.Update(gameTime);
            GameRoot.Instance.Foregrounds.Update(gameTime);

            if (sceneState != GameStateType.None)
                _newState = sceneState;

            return _newState;
        }

        public override void Draw()
        {
            GameRoot.Instance.Backgrounds.Draw();
            _sceneManager.DrawGame();
            GameRoot.Instance.Foregrounds.Draw();
            _sceneManager.DrawUI();
        }
    }
}
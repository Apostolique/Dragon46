using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Apos.Input;

namespace GameProject {
    public class GameRoot : Game {
        protected GameState _currentState;

        public GameRoot() {
            _graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize() {
            // TODO: Add your initialization logic here
            Window.AllowUserResizing = true;

            base.Initialize();
        }

        protected override void LoadContent() {
            _currentState = new MainMenuState(GraphicsDevice);

            _s = new SpriteBatch(GraphicsDevice);

            Assets.Setup(Content);
            _backgrounds = new Backgrounds(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            InputHelper.Setup(this);
        }

        protected override void Update(GameTime gameTime) {
            InputHelper.UpdateSetup();

            if (Triggers.Quit.Pressed())
                Exit();

            var nextState = _currentState.Update(gameTime);

            switch (nextState)
            {
                case GameStateType.MainMenu:
                    {
                        _currentState = new MainMenuState(GraphicsDevice);
                    }
                    break;

                case GameStateType.InGame:
                    {
                        _currentState = new InGameState(GraphicsDevice);
                    }
                    break;
            }

            // TODO: Add your update logic here
            CameraWrapper.Update(gameTime);

            InputHelper.UpdateCleanup();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);

            _currentState.Draw();

            // TODO: Add your drawing code here
            _backgrounds.Draw();

            base.Draw(gameTime);
        }

        GraphicsDeviceManager _graphics;
        SpriteBatch _s;
        Backgrounds _backgrounds;
    }
}
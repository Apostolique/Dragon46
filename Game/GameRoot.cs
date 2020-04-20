using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Apos.Input;

namespace GameProject {
    public class GameRoot : Game
    {
        protected GameState _currentState;
        protected GraphicsDeviceManager _graphics;
        protected SpriteBatch _spriteBatch;

        public GameRoot()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                GraphicsProfile = GraphicsProfile.HiDef,
            };

            IsMouseVisible = true;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //Window.AllowUserResizing = true;
            Window.AllowUserResizing = false;

            _graphics.PreferredBackBufferWidth = 1600;
            _graphics.PreferredBackBufferHeight = 900;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            InputHelper.Setup(this);
            Database.Load();
            Assets.Setup(Content);

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //_currentState = new MainMenuState(GraphicsDevice);
            _currentState = new InGameState(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
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

                case GameStateType.GameOver:
                    {
                        _currentState = new GameOverState(GraphicsDevice);
                    }
                    break;

                case GameStateType.PlayerWon:
                    {
                        _currentState = new PlayerWonState(GraphicsDevice);
                    }
                    break;
            }

            Assets.SoundManager.Update();
            ScreenEffectsManager.Update(gameTime);

            InputHelper.UpdateCleanup();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _currentState.Draw();

            _spriteBatch.Begin(sortMode: SpriteSortMode.Deferred, blendState: BlendState.AlphaBlend);
            ScreenEffectsManager.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
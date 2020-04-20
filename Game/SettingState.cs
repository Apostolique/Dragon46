using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject
{
    public class SettingState : GameState
    {
        protected GameStateType _newState = GameStateType.None;

        protected UILabel _musicVolumeLabel;
        protected UILabel _sfxVolumeLabel;
        protected UIButton _musicUp;
        protected UIButton _musicDown;
        protected UIButton _sfxUp;
        protected UIButton _sfxDown;

        public SettingState(GraphicsDevice graphics) : base(graphics)
        {
            //Assets.SoundManager.StopAll();

            UIHelper.Clear();

            var titleLabel = new UILabel("Settings", 60, 5);
            titleLabel.PinTop(graphics, 50);
            titleLabel.CenterX(graphics);

            _musicVolumeLabel = new UILabel("", 60, 5);
            _musicVolumeLabel.Position.Y = 350;

            _sfxVolumeLabel = new UILabel("", 60, 5);
            _sfxVolumeLabel.Position.Y = 425;

            var startButton = new UIButton("> Back to Menu", 42, 5)
            {
                OnClick = () => { _newState = GameStateType.MainMenu; }
            };
            startButton.CenterX(graphics);
            startButton.Position.Y = 550;

            _musicUp = new UIButton("+", 42, 5)
            {
                OnClick = () =>
                {
                    Assets.MusicVolume += 0.05f;
                    UpdateVolumeLabels();
                }
            };

            _musicDown = new UIButton("-", 42, 5)
            {
                OnClick = () =>
                {
                    Assets.MusicVolume -= 0.05f;
                    UpdateVolumeLabels();
                }
            };

            _sfxUp = new UIButton("+", 42, 5)
            {
                OnClick = () =>
                {
                    Assets.SFXVolume += 0.05f;
                    UpdateVolumeLabels();
                }
            };

            _sfxDown = new UIButton("-", 42, 5)
            {
                OnClick = () =>
                {
                    Assets.SFXVolume -= 0.05f;
                    UpdateVolumeLabels();
                }
            };

            UIHelper.AddButton(startButton);
            UIHelper.AddButton(_musicUp);
            UIHelper.AddButton(_musicDown);
            UIHelper.AddButton(_sfxUp);
            UIHelper.AddButton(_sfxDown);
            UIHelper.AddLabel(titleLabel);
            UIHelper.AddLabel(_musicVolumeLabel);
            UIHelper.AddLabel(_sfxVolumeLabel);

            UpdateVolumeLabels();
        }

        protected void UpdateVolumeLabels()
        {
            if (Assets.MusicVolume > 1)
                Assets.MusicVolume = 1;
            if (Assets.MusicVolume < 0)
                Assets.MusicVolume = 0;

            if (Assets.SFXVolume > 1)
                Assets.SFXVolume = 1;
            if (Assets.SFXVolume < 0)
                Assets.SFXVolume = 0;

            int musicVol = (int)(100 * Assets.MusicVolume);
            int sfxVol = (int)(100 * Assets.SFXVolume);

            _musicVolumeLabel.UpdateLabel("Music volume: " + musicVol.ToString(), 42, 5);
            _musicVolumeLabel.CenterX(_graphics);

            _sfxVolumeLabel.UpdateLabel("Effect volume: " + sfxVol.ToString(), 42, 5);
            _sfxVolumeLabel.CenterX(_graphics);

            var buttonPadding = 10;
            _musicUp.Position.X = _musicVolumeLabel.Position.X + _musicVolumeLabel.Width + buttonPadding;
            _musicUp.Position.Y = _musicVolumeLabel.Position.Y;
            _musicDown.Position.X = _musicUp.Position.X + _musicUp.Width + buttonPadding;
            _musicDown.Position.Y = _musicVolumeLabel.Position.Y;
            _sfxUp.Position.X = _sfxVolumeLabel.Position.X + _sfxVolumeLabel.Width + buttonPadding;
            _sfxUp.Position.Y = _sfxVolumeLabel.Position.Y;
            _sfxDown.Position.X = _sfxUp.Position.X + _sfxUp.Width + buttonPadding;
            _sfxDown.Position.Y = _sfxVolumeLabel.Position.Y;

            Assets.UpdateVolume();
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
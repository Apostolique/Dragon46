using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject {
    public class InfiniteObjects {
        public InfiniteObjects(Texture2D texture, float z, float scale, float? limitY = null) {
            Texture = texture;
            Z = z;
            Scale = scale;
            LimitY = limitY;
        }

        public Texture2D Texture {
            get;
            set;
        }
        public float Z {
            get;
            set;
        }
        public float Scale {
            get;
            set;
        }
        public float? LimitY {
            get;
            set;
        }

        public void Draw(SpriteBatch s) {
            Rectangle view = s.GraphicsDevice.Viewport.Bounds;
            Rectangle window;

            if (LimitY.HasValue) {
                Vector2 max = CameraWrapper.Camera.WorldToScreen(0, LimitY.Value, Z);
                window = new Rectangle(0, (int)max.Y, view.Width, view.Height - (int)max.Y);
            } else {
                window = view;
            }

            Vector2 size = new Vector2(Texture.Width, Texture.Height);
            var posOffset = new Vector2(0, 0);

            Matrix m =
                Matrix.CreateScale(Scale) *
                Matrix.CreateScale(size.X, size.Y, 1) *
                Matrix.CreateTranslation(posOffset.X, posOffset.Y, 1) *
                CameraWrapper.Camera.View(Z) *
                Matrix.CreateScale(1f / size.X, 1f / size.Y, 1);

            Assets.Infinite.Parameters["ScrollMatrix"].SetValue(Matrix.Invert(m));
            Assets.Infinite.Parameters["ViewportSize"].SetValue(new Vector2(view.Width, view.Height));

            s.Begin(samplerState: SamplerState.LinearWrap, effect: Assets.Infinite);
            s.Draw(Texture, new Vector2(window.X, window.Y), window, Color.White);
            s.End();
        }
    }
}
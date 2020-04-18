using System.Linq;
using Dcrew.MonoGame._2D_Spatial_Partition;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject {
    public class Backgrounds {
        public Backgrounds(GraphicsDevice g) {
            _s = new SpriteBatch(g);
            Quadtree<BackgroundObjects>.Add(new BackgroundObjects(0, 0, -4, Assets.Bush));
            Quadtree<BackgroundObjects>.Add(new BackgroundObjects(0, 0, -3, Assets.Bush));
            Quadtree<BackgroundObjects>.Add(new BackgroundObjects(400, 0, -2, Assets.Bush));
            Quadtree<BackgroundObjects>.Add(new BackgroundObjects(-300, 0, -1, Assets.Bush));
        }

        public void Draw() {
            Rectangle view = _s.GraphicsDevice.Viewport.Bounds;
            Vector2 size = new Vector2(Assets.Sky.Width, Assets.Sky.Height);
            var scale = 10;
            var posOffset = new Vector2(0, 0);

            Matrix m =
                Matrix.CreateScale(scale) *
                Matrix.CreateScale(size.X, size.Y, 1) *
                Matrix.CreateTranslation(posOffset.X, posOffset.Y, 1) *
                CameraWrapper.Camera.View(-10) *
                Matrix.CreateScale(1f / size.X, 1f / size.Y, 1);

            Assets.Infinite.Parameters["ScrollMatrix"].SetValue(Matrix.Invert(m));
            Assets.Infinite.Parameters["ViewportSize"].SetValue(new Vector2(view.Width, view.Height));

            _s.Begin(samplerState: SamplerState.LinearWrap, effect: Assets.Infinite);
            _s.Draw(Assets.Sky, new Vector2(0, 0), view, Color.White);
            _s.End();

            var byLayer = Quadtree<BackgroundObjects>.Query(Quadtree<BackgroundObjects>.Bounds).GroupBy(
                backgroundObject => backgroundObject.Z,
                backgroundObject => backgroundObject,
                (z, backgroundObjects) => new {
                    Z = z,
                    BackgroundObjects = backgroundObjects
                }
            ).OrderBy(l => l.Z);

            foreach (var l in byLayer) {
                _s.Begin(transformMatrix: CameraWrapper.Camera.View(l.Z));
                foreach (var e in l.BackgroundObjects)
                    e.Draw(_s);
                _s.End();
            }
        }

        SpriteBatch _s;
    }
}
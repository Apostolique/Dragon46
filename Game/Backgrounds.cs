using System.Linq;
using Dcrew.MonoGame._2D_Spatial_Partition;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace GameProject {
    public class Backgrounds {
        public Backgrounds(GraphicsDevice g) {
            _s = new SpriteBatch(g);
            Quadtree<BackgroundObjects>.Add(new BackgroundObjects(0, 0, -2, 100, 200, Color.Red));
            Quadtree<BackgroundObjects>.Add(new BackgroundObjects(0, 0, -1, 100, 200, Color.Beige));
            Quadtree<BackgroundObjects>.Add(new BackgroundObjects(0, 0, 0, 100, 200, Color.Blue));
        }

        public void Draw() {
            var byLayer = Quadtree<BackgroundObjects>.Query(Quadtree<BackgroundObjects>.Bounds).GroupBy(
                backgroundObject => backgroundObject.Z,
                backgroundObject => backgroundObject,
                (z, backgroundObjects) => new {
                    Z = z,
                    BackgroundObjects = backgroundObjects
                }
            );

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
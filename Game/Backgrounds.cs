using System.Linq;
using Dcrew.MonoGame._2D_Spatial_Partition;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject {
    public class Backgrounds {
        public Backgrounds(GraphicsDevice g) {
            _s = new SpriteBatch(g);
            Quadtree<BackgroundObjects>.Add(new BackgroundObjects(0, 0, -2, Assets.Bush));
            Quadtree<BackgroundObjects>.Add(new BackgroundObjects(0, 0, -1, Assets.Bush));
            Quadtree<BackgroundObjects>.Add(new BackgroundObjects(0, 0, 0, Assets.Bush));
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
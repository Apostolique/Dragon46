using Dcrew.MonoGame._2D_Spatial_Partition;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject {
    public class Backgrounds {
        public Backgrounds(GraphicsDevice g) {
            _s = new SpriteBatch(g);
            Quadtree<BackgroundObjects>.Add(new BackgroundObjects(0, 0, 100, 200));
        }

        public void Draw() {
            _s.Begin(transformMatrix: CameraWrapper.Camera.View(0));
            foreach (var e in Quadtree<BackgroundObjects>.Query(Quadtree<BackgroundObjects>.Bounds))
                e.Draw(_s);
            _s.End();
        }

        SpriteBatch _s;
    }
}
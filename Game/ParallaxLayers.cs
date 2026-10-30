using System.Collections.Generic;
using Apos.Spatial;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject {
    /// <summary>
    /// Objects grouped by Z. Each Z gets its own tree since the camera sees a different part of
    /// the world at each Z. Only what's in view gets drawn.
    /// </summary>
    public class ParallaxLayers {
        public void Add(BackgroundObjects o) {
            if (!_layers.TryGetValue(o.Z, out var tree)) {
                tree = new AABBTree<BackgroundObjects>();
                _layers.Add(o.Z, tree);
            }
            tree.Add(o.XY, o.Size, o);
            _objects.Add(o);
        }

        public void Update(GameTime gameTime) {
            foreach (var o in _objects) {
                o.Update(gameTime);
                if (o.Moves)
                    _layers[o.Z].Update(o, o.XY, o.Size);
            }
        }

        public void Draw(SpriteBatch s) {
            foreach (var layer in _layers) {
                var view = CameraWrapper.Camera.GetViewRect(layer.Key);

                _visible.Clear();
                foreach (var o in layer.Value.Query(new Vector2(view.X, view.Y), new Vector2(view.Width, view.Height)))
                    _visible.Add(o);
                if (_visible.Count == 0)
                    continue;
                _visible.Sort(BackgroundObjects.CompareDepth);

                s.Begin(transformMatrix: CameraWrapper.Camera.GetView(layer.Key));
                foreach (var o in _visible)
                    o.Draw(s);
                s.End();
            }
        }

        SortedList<float, AABBTree<BackgroundObjects>> _layers = new SortedList<float, AABBTree<BackgroundObjects>>();
        List<BackgroundObjects> _objects = new List<BackgroundObjects>();
        List<BackgroundObjects> _visible = new List<BackgroundObjects>();
    }
}

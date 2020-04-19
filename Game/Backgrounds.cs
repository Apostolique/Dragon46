using System;
using System.Collections.Generic;
using System.Linq;
using Dcrew.MonoGame._2D_Spatial_Partition;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject {
    public class Backgrounds {
        public Backgrounds(GraphicsDevice g) {
            int mapLength = 20000;

            _s = new SpriteBatch(g);

            _infiniteObjects.Add(new InfiniteObjects(Assets.Sky, _furthest - 10, 100, Vector2.Zero));
            _infiniteObjects.Add(new InfiniteObjects(Assets.Mountains, _furthest, 2, new Vector2(0, 450), -260));

            var groundScale = 1;

            var maxDepth = 0;

            for (int i = _furthest + 1; i <= 0; i++) {
                for (int j = 0; j < mapLength; j += (Assets.Ground.Width - 100) * groundScale) {
                    maxDepth++;
                    Quadtree<BackgroundObjects>.Add(new BackgroundObjects(j, 0, i, groundScale, Assets.Ground, false, maxDepth));
                }
            }

            for (int i = 0; i < 130; i++) {
                var scale = (float)(Core.R.NextDouble() * 0.8 + 0.2);
                Quadtree<BackgroundObjects>.Add(new BackgroundObjects(Core.R.Next(0, mapLength), -(int)(Assets.Tree.Height * scale) + 250, Core.R.Next(_furthest + 1, 1), scale, Assets.Tree, true, maxDepth + (int)((1 - scale) * 100)));
            }
        }

        public void Update(GameTime gameTime) {
            foreach (var e in Quadtree<BackgroundObjects>.Query(Quadtree<BackgroundObjects>.Bounds))
                e.Update(gameTime);
        }

        public void Draw() {
            foreach (var e in _infiniteObjects)
                e.Draw(_s);

            var byLayer = Quadtree<BackgroundObjects>.Query(Quadtree<BackgroundObjects>.Bounds).GroupBy(
                backgroundObject => backgroundObject.Z,
                backgroundObject => backgroundObject,
                (z, backgroundObjects) => new {
                    Z = z,
                    BackgroundObjects = backgroundObjects.OrderBy(o => o.Depth)
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
        List<InfiniteObjects> _infiniteObjects = new List<InfiniteObjects>();
        int _furthest = -5;
    }
}
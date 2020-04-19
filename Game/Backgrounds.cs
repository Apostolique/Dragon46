using System;
using System.Collections.Generic;
using System.Linq;
using Dcrew.MonoGame._2D_Spatial_Partition;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject {
    public class Backgrounds {
        public Backgrounds(GraphicsDevice g) {
            _r = new Random();
            int mapLength = 20000;

            _s = new SpriteBatch(g);

            _infiniteObjects.Add(new InfiniteObjects(Assets.Sky, _furthest, 10));

            var groundScale = 1;

            for (int i = _furthest + 1; i <= 0; i++) {
                for (int j = 0; j < mapLength; j += (Assets.Ground.Width - 100) * groundScale) {
                    Quadtree<BackgroundObjects>.Add(new BackgroundObjects(j, 0, i, groundScale, Assets.Ground, j));
                }
            }

            for (int i = 0; i < 300; i++) {
                Quadtree<BackgroundObjects>.Add(new BackgroundObjects(_r.Next(0, mapLength), -Assets.Bush.Height, _r.Next(_furthest + 1, 0), 1f, Assets.Bush));
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
        Random _r;
        List<InfiniteObjects> _infiniteObjects = new List<InfiniteObjects>();
        int _furthest = -5;
    }
}
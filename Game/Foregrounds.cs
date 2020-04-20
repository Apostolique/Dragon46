using System;
using System.Collections.Generic;
using System.Linq;
using Dcrew.MonoGame._2D_Spatial_Partition;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject {
    public class Foregrounds {
        public Foregrounds(GraphicsDevice g) {
            int mapLength = 20000;

            _s = new SpriteBatch(g);

            var maxDepth = 0;

            var items = new List<(Texture2D Texture, Core.AnimationStyle Style)> {
                (Assets.Shrub1, Core.AnimationStyle.Tree),
                (Assets.Shrub2, Core.AnimationStyle.Tree),
                (Assets.Shrub3, Core.AnimationStyle.Tree),
            };

            for (int i = 0; i < 100; i++) {
                var scale = (float)(Core.R.NextDouble() * 0.4 + 0.6) * 0.7f;
                var type = items[Core.R.Next(0, items.Count)];
                var z = Core.R.Next(0, 4) / 10f + 0.2f;
                Quadtree<ForegroundObjects>.Add(new ForegroundObjects(Core.R.Next(0, mapLength), -(int)(type.Texture.Height * scale) + 300, z, scale, type.Texture, type.Style, maxDepth + (int)((1 - scale) * 100)));
            }
        }

        public void Update(GameTime gameTime) {
            foreach (var e in Quadtree<ForegroundObjects>.Query(Quadtree<ForegroundObjects>.Bounds))
                e.Update(gameTime);
        }

        public void Draw() {
            var byLayer = Quadtree<ForegroundObjects>.Query(Quadtree<ForegroundObjects>.Bounds).GroupBy(
                foregroundObject => foregroundObject.Z,
                foregroundObject => foregroundObject,
                (z, foregroundObject) => new {
                    Z = z,
                    ForegroundObjects = foregroundObject.OrderBy(o => o.Depth)
                }
            ).OrderBy(l => l.Z);

            foreach (var l in byLayer) {
                _s.Begin(transformMatrix: CameraWrapper.Camera.View(l.Z));
                foreach (var e in l.ForegroundObjects)
                    e.Draw(_s);
                _s.End();
            }
        }

        SpriteBatch _s;
        int _furthest = -5;
    }
}
using System;
using System.Collections.Generic;
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
                _layers.Add(new BackgroundObjects(Core.R.Next(0, mapLength), -(int)(type.Texture.Height * scale) + 300, z, scale, type.Texture, type.Style, maxDepth + (int)((1 - scale) * 100)));
            }
        }

        public void Update(GameTime gameTime) {
            _layers.Update(gameTime);
        }

        public void Draw() {
            _layers.Draw(_s);
        }

        SpriteBatch _s;
        ParallaxLayers _layers = new ParallaxLayers();
        int _furthest = -5;
    }
}
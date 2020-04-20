using Microsoft.Xna.Framework.Graphics;

namespace GameProject {
    public class ForegroundObjects : BackgroundObjects {
        public ForegroundObjects(int x, int y, float z, float scale, Texture2D texture, Core.AnimationStyle style, int depth = 0) : base(x, y, z, scale, texture, style, depth) { }
    }
}
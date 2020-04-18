using Dcrew.MonoGame._2D_Spatial_Partition;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace GameProject {
    public class BackgroundObjects : IAABB {
        public BackgroundObjects(int x, int y, int width, int height) {
            _aabb = new Rectangle(x, y, width, height);
        }

        public Rectangle AABB => _aabb;
        public float Angle => 0;
        public Vector2 Origin => Vector2.Zero;

        public void Draw(SpriteBatch s) {
            s.FillRectangle(AABB, Color.White);
        }

        private Rectangle _aabb;

    }
}
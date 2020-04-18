using Dcrew.MonoGame._2D_Spatial_Partition;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace GameProject {
    public class BackgroundObjects : IAABB {
        public BackgroundObjects(int x, int y, float z, int width, int height, Color c) {
            _aabb = new Rectangle(x, y, width, height);
            Z = z;
            Color = c;
        }

        public Rectangle AABB => _aabb;
        public float Angle => 0;
        public Vector2 Origin => Vector2.Zero;
        public Color Color {
            get;
            set;
        }

        public float Z {
            get;
            set;
        } = 0;

        public void Draw(SpriteBatch s) {
            s.FillRectangle(AABB, Color);
        }

        private Rectangle _aabb;

    }
}
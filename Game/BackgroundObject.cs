using Dcrew.MonoGame._2D_Spatial_Partition;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace GameProject {
    public class BackgroundObjects : IAABB {
        public BackgroundObjects(int x, int y, float z, Texture2D texture) {
            Z = z;
            Texture = texture;
            _aabb = new Rectangle(x, y, Texture.Width, Texture.Height);
        }

        public Texture2D Texture {
            get;
            set;
        }
        public Rectangle AABB => _aabb;
        public float Angle => 0;
        public Vector2 Origin => Vector2.Zero;

        public float Z {
            get;
            set;
        } = 0;

        public void Draw(SpriteBatch s) {
            s.Draw(Texture, AABB, Color.White);
        }

        Rectangle _aabb;
    }
}
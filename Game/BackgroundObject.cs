using Dcrew.MonoGame._2D_Spatial_Partition;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject {
    public class BackgroundObjects : IAABB {
        public BackgroundObjects(int x, int y, float z, float scale, Texture2D texture, int depth = 0) {
            Z = z;
            Texture = texture;
            _aabb = new Rectangle(x, y, (int)(Texture.Width * scale), (int)(Texture.Height * scale));
            Depth = depth;
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
        public int Depth {
            get;
            set;
        }

        public void Update(GameTime gameTime) {

        }

        public void Draw(SpriteBatch s) {
            s.Draw(Texture, AABB, Texture.Bounds, Color.White, Angle, Vector2.Zero, SpriteEffects.None, 0);
        }

        Rectangle _aabb;
    }
}
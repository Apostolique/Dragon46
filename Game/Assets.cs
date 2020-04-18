using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject {
    public static class Assets {
        public static void Setup(ContentManager content) {
            Bush = content.Load<Texture2D>("bush");
        }

        public static Texture2D Bush;
    }
}
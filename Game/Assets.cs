using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject {
    public static class Assets {
        public static void Setup(ContentManager content) {
            Infinite = content.Load<Effect>("infinite");
            Sky = content.Load<Texture2D>("sky");
            Bush = content.Load<Texture2D>("bush");
        }

        public static Effect Infinite;
        public static Texture2D Sky;
        public static Texture2D Bush;
    }
}
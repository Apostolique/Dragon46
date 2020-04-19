using Microsoft.Xna.Framework;

namespace GameProject {
    public static class Core {
        public static void Update(GameTime gameTime) {
            CameraWrapper.Update(gameTime);
        }
    }
}
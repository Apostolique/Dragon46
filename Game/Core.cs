using Microsoft.Xna.Framework;
using MonoGame.Extended.Tweening;

namespace GameProject {
    public static class Core {
        public static Tweener Tweener = new Tweener();

        public static void Update(GameTime gameTime) {
            CameraWrapper.Update(gameTime);

            Tweener.Update(gameTime.ElapsedGameTime.Seconds);
        }
    }
}
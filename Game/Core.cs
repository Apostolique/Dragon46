using System;
using Microsoft.Xna.Framework;

namespace GameProject {
    public static class Core {
        public static Random R = new Random();

        public static void Update(GameTime gameTime) {
            CameraWrapper.Update(gameTime);
        }
    }
}
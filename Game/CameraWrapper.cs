using Dcrew.MonoGame._2D_Camera;
using Microsoft.Xna.Framework;

namespace GameProject {
    public static class CameraWrapper {
        static CameraWrapper() {
            Camera = new Camera(new Vector2(2800, -50));
        }

        public static void Update(GameTime gameTime) {
            if (Triggers.CameraLeft.Held()) {
                CameraWrapper.Camera.XY -= new Vector2(0.2f * gameTime.ElapsedGameTime.Milliseconds, 0);
            }
            if (Triggers.CameraRight.Held()) {
                CameraWrapper.Camera.XY += new Vector2(0.2f * gameTime.ElapsedGameTime.Milliseconds, 0);
            }
            if (Triggers.CameraUp.Held()) {
                CameraWrapper.Camera.XY -= new Vector2(0, 0.2f * gameTime.ElapsedGameTime.Milliseconds);
            }
            if (Triggers.CameraDown.Held()) {
                CameraWrapper.Camera.XY += new Vector2(0, 0.2f * gameTime.ElapsedGameTime.Milliseconds);
            }
        }

        public static void GoToNextEncounter() {

        }

        public static Camera Camera;
    }
}
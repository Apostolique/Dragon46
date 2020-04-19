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
            if (Triggers.CameraDown.Held()) {
                CameraWrapper.Camera.XY += new Vector2(0, 0.2f * gameTime.ElapsedGameTime.Milliseconds);
            }

            if (Triggers.CameraNext.Pressed()) {
                GoToNextEncounter();
            }

            if (CameraTween != null) {
                if (!CameraTween.Update(gameTime)) {
                    Camera.X = CameraTween.Value;
                } else {
                    CameraTween = null;
                }
            }
        }

        public static void GoToNextEncounter() {
            CameraTween = new Tween(2000, Camera.X, Camera.X + 2000, false);
        }

        public static Camera Camera;
        private static Tween CameraTween;
    }
}
using Dcrew.MonoGame._2D_Camera;
using Microsoft.Xna.Framework;

namespace GameProject {
    public static class CameraWrapper {
        static CameraWrapper() {
            Camera = new Camera(new Vector2(2800, -50));
        }

        public static void Update(GameTime gameTime) {
            if (Triggers.CameraLeft.Pressed()) {
                GoToPrevEncounter(2000);
            }
            if (Triggers.CameraRight.Held()) {
                GoToNextEncounter(2000);
            }

            if (CameraTween != null) {
                if (!CameraTween.Update(gameTime)) {
                    Camera.X = CameraTween.Value;
                } else {
                    CameraTween = null;
                }
            }
        }

        public static void GoToPrevEncounter(float duration, float distance = 2000) {
            CameraTween = new Tween(duration, Camera.X, Camera.X - distance, false);
        }
        public static void GoToNextEncounter(float duration, float distance = 2000) {
            CameraTween = new Tween(duration, Camera.X, Camera.X + distance, false);
        }

        public static Camera Camera;
        private static Tween CameraTween;
    }
}
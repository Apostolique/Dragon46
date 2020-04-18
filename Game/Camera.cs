using Dcrew.MonoGame._2D_Camera;
using Microsoft.Xna.Framework;

namespace GameProject {
    public static class CameraWrapper {
        static CameraWrapper() {
            Camera = new Camera(new Vector2(0, 0));
        }

        public static Camera Camera;
    }
}
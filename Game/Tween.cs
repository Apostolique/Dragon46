using System;
using Microsoft.Xna.Framework;

namespace GameProject {
    public class Tween {
        public Tween(float duration, float start, float target, bool bounce = false) {
            Duration = duration;
            Start = start;
            Target = target;
            Bounce = bounce;
        }

        public bool Bounce {
            get;
            set;
        }
        public float Duration {
            get;
            set;
        }
        public float CurrentTime {
            get;
            set;
        }
        public float Value => MathHelper.Lerp(Start, Target, Easing(_percent));
        public float Start {
            get;
            set;
        }
        public float Target {
            get;
            set;
        }

        public Func<float, float> Easing {
            get;
            set;
        } = EasingFunctions.QuadraticInOut;

        /// <returns>true when animation is done.</returns>
        public bool Update(GameTime gameTime) {
            CurrentTime += gameTime.ElapsedGameTime.Milliseconds;

            if (CurrentTime > Duration) {
                if (Bounce) {
                    swap();
                } else {
                    return true;
                }
            }
            return false;
        }

        private void swap() {
            var temp = Start;
            Start = Target;
            Target = temp;
            CurrentTime = 0;
        }

        private float _percent => CurrentTime / Duration;
    }
}
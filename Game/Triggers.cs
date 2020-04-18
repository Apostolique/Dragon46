using Apos.Input;
using Microsoft.Xna.Framework.Input;

namespace GameProject {
    public static class Triggers {
        public static ConditionComposite Quit =
            new ConditionComposite(
                new ConditionSet(new ConditionKeyboard(Keys.Escape)),
                new ConditionSet(new ConditionGamePad(GamePadButton.Back, 0))
            );
        public static ConditionComposite CameraLeft =
            new ConditionComposite(
                new ConditionSet(new ConditionKeyboard(Keys.Left))
            );
        public static ConditionComposite CameraRight =
            new ConditionComposite(
                new ConditionSet(new ConditionKeyboard(Keys.Right))
            );
    }
}
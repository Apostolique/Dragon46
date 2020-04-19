using Apos.Input;
using Microsoft.Xna.Framework.Input;

namespace GameProject
{
    public static class Triggers
    {
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
        public static ConditionComposite CameraUp =
            new ConditionComposite(
                new ConditionSet(new ConditionKeyboard(Keys.Up))
            );
        public static ConditionComposite CameraDown =
            new ConditionComposite(
                new ConditionSet(new ConditionKeyboard(Keys.Down))
            );
    }
}
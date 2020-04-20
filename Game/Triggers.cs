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
        public static ConditionComposite PlayerSkill1 =
            new ConditionComposite(
                new ConditionSet(new ConditionKeyboard(Keys.D1))
            );
        public static ConditionComposite PlayerSkill2 =
            new ConditionComposite(
                new ConditionSet(new ConditionKeyboard(Keys.D2))
            );
        public static ConditionComposite PlayerSkill3 =
            new ConditionComposite(
                new ConditionSet(new ConditionKeyboard(Keys.D3))
            );
        public static ConditionComposite PlayerSkill4 =
            new ConditionComposite(
                new ConditionSet(new ConditionKeyboard(Keys.D4))
            );
        public static ConditionComposite PlayerSkill5 =
            new ConditionComposite(
                new ConditionSet(new ConditionKeyboard(Keys.D5))
            );
        public static ConditionComposite PlayerSkill6 =
            new ConditionComposite(
                new ConditionSet(new ConditionKeyboard(Keys.D6))
            );
        public static ConditionComposite MouseLeftClick =
            new ConditionComposite(
                new ConditionSet(new ConditionMouse(MouseButton.LeftButton))
            );
    }
}
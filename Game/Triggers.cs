using Apos.Input;
using Microsoft.Xna.Framework.Input;

namespace GameProject
{
    public static class Triggers
    {
        public static ICondition Quit =
            new AnyCondition(
                new KeyboardCondition(Keys.Escape),
                new GamePadCondition(GamePadButton.Back, 0)
            );
        public static ICondition CameraLeft = new KeyboardCondition(Keys.Left);
        public static ICondition CameraRight = new KeyboardCondition(Keys.Right);
        public static ICondition PlayerSkill1 = new KeyboardCondition(Keys.D1);
        public static ICondition PlayerSkill2 = new KeyboardCondition(Keys.D2);
        public static ICondition PlayerSkill3 = new KeyboardCondition(Keys.D3);
        public static ICondition PlayerSkill4 = new KeyboardCondition(Keys.D4);
        public static ICondition PlayerSkill5 = new KeyboardCondition(Keys.D5);
        public static ICondition PlayerSkill6 = new KeyboardCondition(Keys.D6);
        public static ICondition MouseLeftClick = new MouseCondition(MouseButton.LeftButton);
    }
}
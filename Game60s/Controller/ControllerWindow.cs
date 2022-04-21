using Game60s.Model;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Game60s.Controller
{
    public static class ControllerWindow
    {
        internal static HashSet<Keys> KeysPressed = new HashSet<Keys>();

        public static void SetPressedKey(Keys pressedKey) =>
            KeysPressed.Add(pressedKey);

        public static void RemoveKey(Keys unPressedKey) => 
            KeysPressed.Remove(unPressedKey);

        public static void EterationGameModel(int timerTick)
        {
            GameModell.player.Act(KeysPressed);
            if (timerTick % 100 == 0)
                GameModell.xyiZnaetKakNazvat();
        }
    }
}

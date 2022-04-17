using Game60s.Model;
using System.Windows.Forms;

namespace Game60s.Controller
{
    public static class ControllerWindow
    {
        public static void SetPressedKey(Keys pressedKey) =>
            GameModell.KeysPressed.Add(pressedKey);
        public static void RemoveKey(Keys unPressedKey) => 
            GameModell.KeysPressed.Remove(unPressedKey);

        public static void EterationGameModel()
        {
            GameModell.Act();
        }
    }
}

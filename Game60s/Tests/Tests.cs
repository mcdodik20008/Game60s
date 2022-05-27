using Game60s.Model;
using Game60s.Controller;
using NUnit.Framework;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace Game60s.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void PlayerFasterBabuin()
        {
            HashSet<Keys> KeysPressed = new HashSet<Keys>() { Keys.Down };
            var strMap = new string[7] {
                "OOOOOOO",
                "OBBBBBO",
                "OBBBBBO",
                "OBBBBBO",
                "OBBBBBO",
                "OBBBBBO",
                "OOOOOOO"};
            SetGameModell(strMap);

            GameModell.Babuin = new Babuin(GameModell.ElementSize, 5 * GameModell.ElementSize);
            GameModell.player = new Player(GameModell.ElementSize,  GameModell.ElementSize);
            GameModell.Resourse = new Resourse[1] { new Stick(GameModell.ElementSize, 3 * GameModell.ElementSize) };
            ControllerWindow.KeysPressed = KeysPressed;
            foreach (var item in Enumerable.Range(0, 1000))
            {
                ControllerWindow.GameProcess(3);
                if (GameModell.player.CountResourse == 1)
                    break;
            }
            Assert.That(GameModell.player.CountResourse == 1 && GameModell.Babuin.CountResourse == 0);

        }

        protected static void SetGameModell(string[] strMap)
        {
            new GameModell(new Map(
                MapCreator.GetMapIEntity(
                    MapCreator.GetMapChar(
                        strMap
                    )
                )
             ));
        }
    }
}

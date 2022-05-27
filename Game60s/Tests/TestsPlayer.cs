using Game60s.Model;
using Game60s.Controller;
using NUnit.Framework;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Game60s.Tests
{
    [TestFixture]
    public class TestsPlayer : Tests
    {
        [Test]
        public void PlayerMovementIsCorrect()
        {
            HashSet<Keys> KeysPressed = new HashSet<Keys>() { Keys.Right, Keys.Down };
            var strMap = new string[5] { "OOOOO", "OBBBO", "OBBBO", "OBBBO", "OOOOO" };
            SetGameModell(strMap);

            GameModell.player = new Player(2 * GameModell.ElementSize, 2 * GameModell.ElementSize);
            GameModell.player.Act(KeysPressed);

            Assert.That(GameModell.player.X == 2 * GameModell.ElementSize + 2);
            Assert.That(GameModell.player.Y == 2 * GameModell.ElementSize + 2);

            KeysPressed.Clear();
            KeysPressed.Add(Keys.Left);
            KeysPressed.Add(Keys.Up);
            GameModell.player.Act(KeysPressed);

            Assert.That(GameModell.player.X == 2 * GameModell.ElementSize);
            Assert.That(GameModell.player.Y == 2 * GameModell.ElementSize);
        }

        [Test]
        public void PlayerMovementIsCorrectWithOcean()
        {
            HashSet<Keys> KeysPressed = new HashSet<Keys>() { Keys.Left, Keys.Up };
            var strMap = new string[3] { "OOO", "OBO", "OOO" };
            SetGameModell(strMap);

            GameModell.player = new Player(GameModell.ElementSize, GameModell.ElementSize);

            Assert.That(GameModell.player.WhereINAXOJYS() is Ground);
            GameModell.player.Act(KeysPressed);

            // Когда доделааешь игрока, должно проходить
            Assert.That(GameModell.player.WhereINAXOJYS() is Ground);
        }

        [Test]
        public void PlayerCanGetResourse()
        {
            var strMap = new string[3] { "OOO", "OBO", "OOO" };
            SetGameModell(strMap);

            GameModell.Resourse = new Resourse[1] { new Stick(GameModell.ElementSize, GameModell.ElementSize) };
            GameModell.player = new Player(GameModell.ElementSize, GameModell.ElementSize);
            ControllerWindow.GameProcess(4);

            // Когда доделааешь игрока, должно проходить
            Assert.That(GameModell.player.CountResourse == 1);
        }
    }
}
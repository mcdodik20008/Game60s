using Game60s.Model;
using Game60s.Controller;
using NUnit.Framework;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Game60s.Tests
{
    [TestFixture]
    public class TestsBabuin
    {
        [Test]
        public void MovementIsCorrectWithOutResourses()
        {
            HashSet<Keys> KeysPressed = new HashSet<Keys>() { };
            var strMap = new string[5] { "OOOOO", "OBBBO", "OBBBO", "OBBBO", "OOOOO" };
            LoadForTest.SetGameModell(strMap);

            GameModell.Resourse = new Resourse[0] { };
            GameModell.Babuin = new Babuin(2 * GameModell.ElementSize, 2 * GameModell.ElementSize);
            GameModell.Babuin.Act(KeysPressed);

            Assert.That(GameModell.Babuin.X == 2 * GameModell.ElementSize);
            Assert.That(GameModell.Babuin.Y == 2 * GameModell.ElementSize);
        }

        [Test]
        public void MovementIsCorrectWithResourse()
        {
            HashSet<Keys> KeysPressed = new HashSet<Keys>() { };
            var strMap = new string[5] { "OOOOO", "OBBBO", "OBBBO", "OBBBO", "OOOOO" };
            LoadForTest.SetGameModell(strMap);

            GameModell.Resourse = new Resourse[1] { new Stick(2 * GameModell.ElementSize, GameModell.ElementSize) };
            GameModell.Babuin = new Babuin(GameModell.ElementSize, GameModell.ElementSize);
            GameModell.Babuin.Act(KeysPressed);

            Assert.That(GameModell.Babuin.X == GameModell.ElementSize + 1, $"{GameModell.Babuin.X} != {GameModell.ElementSize + 1}");
            Assert.That(GameModell.Babuin.Y == GameModell.ElementSize, $"{GameModell.Babuin.Y} != {GameModell.ElementSize}");
        }

        [Test]
        public void MovementIsCorrectWithResourses()
        {
            HashSet<Keys> KeysPressed = new HashSet<Keys>() { };
            var strMap = new string[5] { "OOOOO", "OBBBO", "OBBBO", "OBBBO", "OOOOO" };
            LoadForTest.SetGameModell(strMap);

            GameModell.Resourse = new Resourse[2]
            {
                new Stick(GameModell.ElementSize, 2 * GameModell.ElementSize),
                new Stick(3 * GameModell.ElementSize, GameModell.ElementSize)
            };
            GameModell.Babuin = new Babuin(GameModell.ElementSize, GameModell.ElementSize);
            GameModell.Babuin.Act(KeysPressed);

            Assert.That(GameModell.Babuin.X == GameModell.ElementSize, $"{GameModell.Babuin.X} != {GameModell.ElementSize}");
            Assert.That(GameModell.Babuin.Y == GameModell.ElementSize + 1, $"{GameModell.Babuin.Y} != {GameModell.ElementSize + 1}");
        }

        [Test]
        public void CanGetResourse()
        {
            var strMap = new string[3] { "OOO", "OBO", "OOO" };
            LoadForTest.SetGameModell(strMap);

            GameModell.Resourse = new Resourse[1] { new Stick(GameModell.ElementSize, GameModell.ElementSize) };
            GameModell.Babuin = new Babuin(GameModell.ElementSize, GameModell.ElementSize);

            ControllerWindow.GameProcess(4);

            // Когда доделааешь игрока, должно проходить
            Assert.That(GameModell.Babuin.CountResourse == 1);
        }
    }
}

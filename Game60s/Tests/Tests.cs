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
            LoadForTest.SetGameModell(strMap);

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

        [Test]
        public void ResourseNotDisposeOnDirt()
        {
            var strMap = new string[3] { "OOO", "OBO", "OOO" };
            LoadForTest.SetGameModell(strMap);
            (GameModell.Map[1, 1] as Ground).Height = 1000;
            GameModell.Resourse = new Resourse[1] {new Stick(GameModell.ElementSize+1, GameModell.ElementSize+1)};
            ControllerWindow.GameProcess(GameModell.TickToWaterLineUp);
            Assert.That(GameModell.Resourse[0] != null);
        }

        [Test]
        public void ResourseDisposeOnOceanLeft()
        {
            var strMap = new string[3] { "OOO", "OBO", "OOO" };
            LoadForTest.SetGameModell(strMap);
            GameModell.Resourse = new Resourse[1] { new Stick(GameModell.ElementSize-1, GameModell.ElementSize) };
            ControllerWindow.GameProcess(GameModell.TickToWaterLineUp);
            Assert.That(GameModell.Resourse[0] == null);
        }

        [Test]
        public void ResourseDisposeOnOceanUp()
        {
            var strMap = new string[3] { "OOO", "OBO", "OOO" };
            LoadForTest.SetGameModell(strMap);
            GameModell.Resourse = new Resourse[1] { new Stick(GameModell.ElementSize, GameModell.ElementSize - 1) };
            ControllerWindow.GameProcess(GameModell.TickToWaterLineUp);
            Assert.That(GameModell.Resourse[0] == null);
        }

        [Test]
        public void ResourseDisposeOnOceanDown()
        {
            var strMap = new string[3] { "OOO", "OBO", "OOO" };
            LoadForTest.SetGameModell(strMap);
            GameModell.Resourse = new Resourse[1] { new Stick(GameModell.ElementSize + 23, GameModell.ElementSize) };
            ControllerWindow.GameProcess(GameModell.TickToWaterLineUp);
            Assert.That(GameModell.Resourse[0] == null);
        }

        [Test]
        public void ResourseDisposeOnOceanRigth()
        {
            var strMap = new string[3] { "OOO", "OBO", "OOO" };
            LoadForTest.SetGameModell(strMap);
            GameModell.Resourse = new Resourse[1] { new Stick(GameModell.ElementSize, GameModell.ElementSize + 23) };
            ControllerWindow.GameProcess(GameModell.TickToWaterLineUp);
            Assert.That(GameModell.Resourse[0] == null);
        }

        [Test]
        public void RealGameResourseDisposeOnOcean()
        {
            var strMap = new string[7] {
                "BBBBBBB",
                "BBBBBBB",
                "BBBBBBB",
                "BBBBBBB",
                "BBBBBBB",
                "BBBBBBB",
                "BBBBBBB"};
            LoadForTest.SetGameModell(strMap);
            LoadForTest.SetHeigthIsLineToUp(GameModell.Map);
            GameModell.Resourse = new Resourse[7];
            for (int i = 0; i < 7; i++)
                GameModell.Resourse[i] = new Stick(
                    GameModell.ElementSize * i,
                    GameModell.ElementSize * i);

            ControllerWindow.GameProcess(GameModell.TickToWaterLineUp);
            for (int i = 0; i < 7; i++)
            {
                ControllerWindow.GameProcess(GameModell.TickToWaterLineUp);
                Assert.That(GameModell.Resourse[i] == null);
            }
        }

        [Test]
        public void Zaebalo()
        {
            var strMap = new string[2] { "BB", "BB" };
            LoadForTest.SetGameModell(strMap);
            (GameModell.Map[0, 0] as Ground).Height = 0;
            (GameModell.Map[0, 1] as Ground).Height = 1;
            (GameModell.Map[1, 0] as Ground).Height = 2;
            (GameModell.Map[1, 1] as Ground).Height = 3;
            GameModell.Resourse = new Resourse[1] { new Stick(
                    GameModell.ElementSize - GameModell.ElementSize / 2,
                    GameModell.ElementSize - GameModell.ElementSize / 2)};

            ControllerWindow.GameProcess(GameModell.TickToWaterLineUp);
            for (int i = 0; i < 4; i++)
            {
                ControllerWindow.GameProcess(GameModell.TickToWaterLineUp);
                Assert.That(GameModell.Resourse[0] == null);
            }
        }

        [Test]
        public void NeMozetBit()
        {
            var strMap = new string[5] { "BBBBB", "BBBBB", "BBBBB", "BBBBB", "BBBBB"};
            LoadForTest.SetGameModell(strMap);
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    (GameModell.Map[i, j] as Ground).Height = 5;

            (GameModell.Map[3, 3] as Ground).Height = 0;
            (GameModell.Map[3, 4] as Ground).Height = 1;
            (GameModell.Map[4, 3] as Ground).Height = 2;
            (GameModell.Map[4, 4] as Ground).Height = 3;

            GameModell.Resourse = new Resourse[1] { new Stick(
                    GameModell.ElementSize * 4 - GameModell.ElementSize / 2,
                    GameModell.ElementSize * 4 - GameModell.ElementSize / 2)};

            for (int i = 0; i < 4; i++)
            {
                ControllerWindow.GameProcess(GameModell.TickToWaterLineUp);
                Assert.That(GameModell.Resourse[0] == null);
            }
        }

        [Test]
        public void GetItemPoCoordinateGround()
        {
            var strMap = new string[3] { "OOO", "OBO", "OOO" };
            LoadForTest.SetGameModell(strMap);
            Assert.That(GameModell.Map.GetItemPoCoordinate(GameModell.ElementSize, GameModell.ElementSize) is Ground);
            Assert.That(GameModell.Map.GetItemPoCoordinate(GameModell.ElementSize, GameModell.ElementSize * 2 - 1) is Ground);
            Assert.That(GameModell.Map.GetItemPoCoordinate(GameModell.ElementSize * 2 - 1, GameModell.ElementSize) is Ground);
            Assert.That(GameModell.Map.GetItemPoCoordinate(GameModell.ElementSize * 2 - 1, GameModell.ElementSize * 2 - 1) is Ground);
        }

        [Test]
        public void GetItemPoCoordinateOcean1()
        {
            var strMap = new string[3] { "OOO", "OBO", "OOO" };
            LoadForTest.SetGameModell(strMap);
            Assert.That(GameModell.Map.GetItemPoCoordinate(GameModell.ElementSize - 1, GameModell.ElementSize) is Ocean);
            Assert.That(GameModell.Map.GetItemPoCoordinate(GameModell.ElementSize, GameModell.ElementSize - 1) is Ocean);
            Assert.That(GameModell.Map.GetItemPoCoordinate(GameModell.ElementSize * 2, GameModell.ElementSize) is Ocean);
            Assert.That(GameModell.Map.GetItemPoCoordinate(GameModell.ElementSize, GameModell.ElementSize * 2) is Ocean);
        }
        [Test]
        public void GetItemPoCoordinateOcean2()
        {
            var strMap = new string[3] { "OOO", "OBO", "OOO" };
            LoadForTest.SetGameModell(strMap);
            Assert.That(GameModell.Map.GetItemPoCoordinate(GameModell.ElementSize - 1, GameModell.ElementSize - 1) is Ocean);
            Assert.That(GameModell.Map.GetItemPoCoordinate(GameModell.ElementSize * 2, GameModell.ElementSize - 1) is Ocean);
            Assert.That(GameModell.Map.GetItemPoCoordinate(GameModell.ElementSize - 1, GameModell.ElementSize * 2) is Ocean);
            Assert.That(GameModell.Map.GetItemPoCoordinate(GameModell.ElementSize * 2, GameModell.ElementSize * 2) is Ocean);
        }
    }
}

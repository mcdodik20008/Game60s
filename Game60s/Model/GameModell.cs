using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Game60s.Model
{
    public class GameModell
    {
        #region Переменные
        //Сделать рандомную позицию или в центре?
        internal static Player player;
        internal static Raft Raft;
        internal static Babuin Babuin;
        internal static Babuin Babuin2;
        /// Катастрофа
        internal static int TickToWaterLineUp = 500;
        internal static int WaterLine = 0;
        internal static TimeSpan TimeToDisaster = new TimeSpan(0, 1, 0);
        internal static Random Rnd = new Random();
        /// Карта
        internal const int ElementSize = 53;
        internal static Map Map;
        internal static Resourse[] Resourse;
        internal static int ResoutseToRaft = new Random().Next(5, 8);
        /// Прочее
        private static readonly DirectoryInfo imagesDirectory = new DirectoryInfo(@"..\..\Model\Images");
        internal static Dictionary<string, Bitmap> EntityImage = new Dictionary<string, Bitmap>();
        public static GameStates GameState;
        public static int GameLevel = 0;
        private static Level level;
        #endregion

        public enum GameStates
        {
            GameStart,
            GameProcess,
            LevelWin,
            LevelLose
        }

        internal GameModell()
        {
            LoadEntityImages();
            RestartGameModell();
        }

        internal GameModell(Map ForTest)
        {
            Map = ForTest;
            Map.SetMapHeight();
            Map.SwitchBorder();
            player = null;
            Babuin = null;
            Raft = null;
        }

        public static void RestartGameModell()
        {
            GameLevel = 0;
            level = new Level(GameLevel);
            player = (Player)new Player().SetRanomCoordinate();

            Map.SetMapHeight();
            Map.SwitchBorder();

            Resourse = new Stick[Rnd.Next(20, 25)];

            for (int i = 0; i < Resourse.Length; i++)
                Resourse[i] = Stick.CreateRandomXY();
        }

        public static void NextGameModell()
        {
            GameLevel++;
            level = new Level(GameLevel);
            player = (Player)new Player().SetRanomCoordinate();

            Map.SetMapHeight();
            Map.SwitchBorder();

            Resourse = new Stick[
                Rnd.Next(
                    Math.Max(20 - GameLevel, 12),
                    Math.Max(25 - GameLevel, 15)
                    )];

            for (int i = 0; i < Resourse.Length; i++)
                Resourse[i] = Stick.CreateRandomXY();
        }


        internal static void LoadEntityImages()
        {
            foreach (var e in imagesDirectory.GetFiles(@"*.png"))
                EntityImage[e.Name] = (Bitmap)Image.FromFile(e.FullName);
        }

        /// <summary>
        /// Увеличивает увроень воды и затапливает все участки, которые оказались ниже уровня
        /// </summary>
        internal static void IncreaseWaterLine()
        {
            WaterLine++;
            for (int x = 0; x < Map.LengthY; x++)
                for (int y = 0; y < Map.LengthY; y++)
                    if ((Map[x, y] as IMapObject).Height < WaterLine)
                        Map[x, y] = Map[x, y].Die();
        }

        private class Level
        {
            public Level(int gameLevel)
            {
                if (gameLevel > 12)
                {
                    Babuin2 = (Babuin)new Babuin().SetRanomCoordinate();
                    Babuin2.SetFastOrSloyStep(gameLevel % 4 > 2);
                }
            }
        }
    }
}
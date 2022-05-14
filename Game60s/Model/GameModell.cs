using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Game60s.Model
{
    public class GameModell
    {
        //Сделать рандомную позицию или в центре?
        internal static Player player = new Player(3 * ElementSize, 3 * ElementSize);
        internal static Raft Raft = null;
        internal static Babuin Babuin = new Babuin(5 * ElementSize, 5 * ElementSize);
        //internal static Babuin Babuin2 = new Babuin(4 * ElementSize, 4 * ElementSize);
        /// Катастрофа
        internal const int TickToWaterLineUp = 1000;
        internal static int WaterLine = 0;
        internal static TimeSpan TimeToDisaster = new TimeSpan(0, 1, 0);
        internal static Random Rnd = new Random();
        /// Карта
        internal const int ElementSize = 50;
        internal static Map Map;
        internal static List<Resourse> ResoursesOnMap = new List<Resourse>();
        internal static Resourse[] Resourse;
        internal static int ResoutseToRaft = new Random().Next(5, 8);
        /// Прочее
        private static readonly DirectoryInfo imagesDirectory = new DirectoryInfo(@"..\..\Model\Images");
        internal static Dictionary<string, Bitmap> EntityImage = new Dictionary<string, Bitmap>();
        
        // Нужно сделать генератор высоты у блока, в зависимости от предыдущих высот,
        // или делать некий рандом относительно какого-то патерна: заранее раданное значение +- 1 в random.Next();
        // придумать 1-2 патерна и выбирать их рандомно(если их 2)
        internal GameModell()
        {
            LoadEntityImages();
            Map = MapCreator.Create();

            Map.SetMapHeight();
            Resourse = new Stick[Rnd.Next(10, 15)];
            for (int i = 0; i < Resourse.Length; i++)
                Resourse[i] = Stick.CreateRandomXY();
            Map.SwitchBorder();
        }

        internal static void LoadEntityImages()
        {
            foreach (var e in imagesDirectory.GetFiles("*.png"))
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
    }
}
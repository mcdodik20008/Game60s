using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Game60s.Model
{
    public class GameModell
    {
        public static TimeSpan TimeToDisaster = new TimeSpan(0, 1, 0);
        internal static Player player = new Player(3 * ElementSize, 3 * ElementSize);
        public const int ElementSize = 65;
        public static int WaterLine = 0;
        internal static Map Map;
        internal static List<Resourse> ResoursesOnMap = new List<Resourse>();
        private static readonly DirectoryInfo imagesDirectory = new DirectoryInfo(@"..\..\Model\Images");
        public static Dictionary<string, Bitmap> EntityImage = new Dictionary<string, Bitmap>();


        internal GameModell()
        {
            LoadEntityImages();
            Map = MapCreator.Create();
            Map.SwitchBorder();
            Map.SetMapHeight();
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
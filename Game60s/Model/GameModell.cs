using System.Collections.Generic;
using System.Windows.Forms;
using System;
using System.Numerics;

namespace Game60s.Model
{
    public class GameModell
    {
        public static TimeSpan TimeToDisaster = new TimeSpan(0, 1, 0);
        public const int ElementSize = 65;
        public const int SizeVisibleMap = 12;
        internal static Map Map;
        internal static HashSet<Keys> KeysPressed;
        internal GameModell()
        {
            Map = MapCreator.Create();
        }

        public static void Act()
        {
            foreach (IEntity[] row in Map)
                foreach (var item in row)
                    item.Act(KeysPressed);
        }
    }
}
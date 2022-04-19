using System.Collections.Generic;
using System.Windows.Forms;
using System;
using System.Numerics;
using System.Drawing;

namespace Game60s.Model
{
    public class GameModell
    {
        public static TimeSpan TimeToDisaster = new TimeSpan(0, 1, 0);
        internal static Player player = new Player(5*ElementSize, 5*ElementSize);
        public const int ElementSize = 65;
        internal static Map Map;
        internal GameModell()
        {
            Map = MapCreator.Create();
        }
    }
}
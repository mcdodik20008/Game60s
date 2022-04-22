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
        internal static Player player = new Player(3 * ElementSize, 3 * ElementSize);
        public const int ElementSize = 65;
        internal static Map Map;
        internal GameModell()
        {
            Map = MapCreator.Create();
            for (int i = 0; i < Map.LengthX; i++)
            {
                for (int j = 0; j < Map.LengthY; j++)
                {
                    if (Map[i, j] is Border b)
                    {
                        if (Map[i + 1, j + 1] is Ocean && Map[i, j + 1] is Ocean) b.Direction = DirectionType.Up; else
                        if (Map[i - 1, j + 1] is Ocean && Map[i, j + 1] is Ocean) b.Direction = DirectionType.Down; else
                        if (Map[i - 1, j - 1] is Ocean && Map[i, j + 1] is Ocean) b.Direction = DirectionType.Right; else
                        if (Map[i + 1, j + 1] is Ocean && Map[i, j + 1] is Ocean) b.Direction = DirectionType.Left; else
                        if (Map[i, j + 1] is Ocean) b.Direction = DirectionType.Up; else
                        if (Map[i, j - 1] is Ocean) b.Direction = DirectionType.Down; else
                        if (Map[i + 1, j] is Ocean) b.Direction = DirectionType.Right; else
                        if (Map[i - 1, j] is Ocean) b.Direction = DirectionType.Left;
                    }
                }
            }
        }

        internal static void xyiZnaetKakNazvat()
        {
            // не пугайся
            for (int x = 0; x < Map.LengthY; x++)
                for (int y = 0; y < Map.LengthY; y++)
                {
                    if (Map[x, y] is Ocean)
                        continue;

                    var n = 0;
                    for (int i = -1; i <= 1; i++)
                        for (int j = -1; j <= 1; j++)
                            if (Math.Abs(i) != Math.Abs(j) && Map[x + i, y + j] is Ocean)
                                n++;

                    if (n >= 2 && Map[x, y] as Ocean == null)
                        Map[x, y].Hp--;
                }

            for (int x = 0; x < Map.LengthY; x++)
                for (int y = 0; y < Map.LengthY; y++)
                    if (Map[x, y].Hp == 0)
                        Map[x, y] = Map[x, y].Die();
        }

        Func<Map, int, int, DirectionType> TrySetDirection(AEntity aE) => 
    }
}
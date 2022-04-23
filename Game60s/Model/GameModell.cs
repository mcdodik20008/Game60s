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
                    if (Map[i, j] is IBorderElement b)
                    {
                        if (IsBorderElement(i - 1, j, i, j - 1) && IsOcean(i - 1, j - 1)) { b.SetDirection(DirectionType.Up); b.SetBorderType(BorderType.angleInside); continue; };
                        if (IsBorderElement(i - 1, j, i, j + 1) && IsOcean(i - 1, j + 1)) { b.SetDirection(DirectionType.Right); b.SetBorderType(BorderType.angleInside); continue; };
                        if (IsBorderElement(i + 1, j, i, j - 1) && IsOcean(i + 1, j - 1)) { b.SetDirection(DirectionType.Left); b.SetBorderType(BorderType.angleInside); continue; };
                        if (IsBorderElement(i + 1, j, i, j + 1) && IsOcean(i + 1, j + 1)) { b.SetDirection(DirectionType.Down); b.SetBorderType(BorderType.angleInside); continue; };

                        if (IsOcean(i - 1, j) && IsOcean(i, j - 1)) { b.SetDirection(DirectionType.Up); b.SetBorderType(BorderType.angle); continue; };
                        if (IsOcean(i - 1, j) && IsOcean(i, j + 1)) { b.SetDirection(DirectionType.Right); b.SetBorderType(BorderType.angle); continue; };
                        if (IsOcean(i + 1, j) && IsOcean(i, j - 1)) { b.SetDirection(DirectionType.Left); b.SetBorderType(BorderType.angle); continue; };
                        if (IsOcean(i + 1, j) && IsOcean(i, j + 1)) { b.SetDirection(DirectionType.Down); b.SetBorderType(BorderType.angle); continue; };

                        if (IsOcean(i - 1, j)) { b.SetDirection(DirectionType.Up); b.SetBorderType(BorderType.border); continue; };
                        if (IsOcean(i + 1, j)) { b.SetDirection(DirectionType.Down); b.SetBorderType(BorderType.border); continue; };
                        if (IsOcean(i, j - 1)) { b.SetDirection(DirectionType.Left); b.SetBorderType(BorderType.border); continue; };
                        if (IsOcean(i, j + 1)) { b.SetDirection(DirectionType.Right); b.SetBorderType(BorderType.border); continue; };
                    }
                }
            }
        }

        private static bool IsBorderElement(int i, int j, int x, int y)
        {
            return Map[i, j] is IBorderElement && Map[x, y] is IBorderElement;
        }

        private static bool IsOcean(int i, int j) => Map[i, j] is Ocean;
        

        internal static void Do()
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
    }
}
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
            Do2();
        }

        public static void Do2()
        {
            for (int i = 0; i < Map.LengthX; i++)
            {
                for (int j = 0; j < Map.LengthY; j++)
                {
                    if (Map[i, j] is Border b)
                    {
                        //внутренние углы
                        if (IsBorderElement(i - 1, j, i, j - 1) && IsOcean(i - 1, j - 1)) { b.Direction = DirectionType.Up; b.BorderType = BorderType.angleInside; continue; };
                        if (IsBorderElement(i - 1, j, i, j + 1) && IsOcean(i - 1, j + 1)) { b.Direction = DirectionType.Right; b.BorderType = BorderType.angleInside; continue; };
                        if (IsBorderElement(i + 1, j, i, j - 1) && IsOcean(i + 1, j - 1)) { b.Direction = DirectionType.Left; b.BorderType = BorderType.angleInside; continue; };
                        if (IsBorderElement(i + 1, j, i, j + 1) && IsOcean(i + 1, j + 1)) { b.Direction = DirectionType.Down; b.BorderType = BorderType.angleInside; continue; };
                        //внешние углы
                        if (IsOcean(i - 1, j) && IsOcean(i, j - 1)) { b.Direction = DirectionType.Up; b.BorderType = BorderType.angle; continue; };
                        if (IsOcean(i - 1, j) && IsOcean(i, j + 1)) { b.Direction = DirectionType.Right; b.BorderType = BorderType.angle; continue; };
                        if (IsOcean(i + 1, j) && IsOcean(i, j - 1)) { b.Direction = DirectionType.Left; b.BorderType = BorderType.angle; continue; };
                        if (IsOcean(i + 1, j) && IsOcean(i, j + 1)) { b.Direction = DirectionType.Down; b.BorderType = BorderType.angle; continue; };
                        //прямые границы
                        if (IsOcean(i - 1, j)) { b.Direction = DirectionType.Up; b.BorderType = BorderType.border; continue; };
                        if (IsOcean(i + 1, j)) { b.Direction = DirectionType.Down; b.BorderType = BorderType.border; continue; };
                        if (IsOcean(i, j - 1)) { b.Direction = DirectionType.Left; b.BorderType = BorderType.border; continue; };
                        if (IsOcean(i, j + 1)) { b.Direction = DirectionType.Right; b.BorderType = BorderType.border; continue; };
                    }
                }
            }
        }

        private static bool IsOcean(int i, int j) => Map[i, j] is Ocean;
        private static bool IsBorderElement(int i, int j, int x, int y) => Map[i, j] is Border && Map[x, y] is Border;

        //это уберется, когда ты добавишь высоты и затопления клеток.
        // что нжно сделать. Добавить высоту клетки + поднимать "Уровень воды(тоже надо сделать)" в модели
        // и придумать как распределять высоты по клеткам. При подъеме на определенную высоту клетка должна умирать.
        internal static void Do1()
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

                    if (n == 2 && Map[x, y] as Ocean == null)
                        Map[x, y].Hp--;
                    if (n == 3 && Map[x, y] as Ocean == null)
                         Map[x, y] = Map[x, y].Die();
                }

            for (int x = 0; x < Map.LengthY; x++)
                for (int y = 0; y < Map.LengthY; y++)
                    if (Map[x, y].Hp == 0)
                        Map[x, y] = Map[x, y].Die();
        }
    }
}
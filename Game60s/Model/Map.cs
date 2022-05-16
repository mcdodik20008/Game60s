using System;
using System.Collections;

namespace Game60s.Model
{
    internal class Map : IEnumerable
    {
        internal static AEntity[][] Mapp;
        internal int LengthX { get => Mapp.Length; }
        internal int LengthY { get => Mapp.Length; }

        public static Func<int, int, bool> IsWithinMap = (i, j) => i > -1 && j > -1 && i < Mapp.Length * GameModell.ElementSize && j < Mapp.Length * GameModell.ElementSize;

        internal Map(int size)
        {
            Mapp = new AEntity[size][];
            for (int i = 0; i < Mapp.Length; i++)
                Mapp[i] = new AEntity[size];
        }

        internal Map(AEntity[][] mapCells) => Mapp = mapCells;

        internal AEntity this[int i, int j]
        {
            get
            {
                return IsWithinMap(i * GameModell.ElementSize, j * GameModell.ElementSize) ? Mapp[i][j] :
                    new Ocean(i, j);
            }
            set
            {
                Mapp[i][j] = IsWithinMap(i, j) ? value :
                    throw new IndexOutOfRangeException($"Какой ты делаешь.");
            }
        }

        public IEnumerator GetEnumerator()
        {
            foreach (var row in Mapp)
                foreach (var item in row)
                    yield return item;
        }
    }

    internal static class MapExt
    {
        /// <summary>
        /// Видоизменяет "Border" на карте, меняя в нем Type и Direcction, в зависимости от соседних ячеек.
        /// Не создает новые обекты.
        /// </summary>
        /// <param name="Map"></param>
        public static void SwitchBorder(this Map Map)
        {
            for (int i = 0; i < Map.LengthX; i++)
            {
                for (int j = 0; j < Map.LengthY; j++)
                {
                    if (Map[i, j] is Ground b)
                    {
                        //
/*                        if (Map.IsOcean(i, j + 1) && Map.IsOcean(i, j - 1)) { b.Die(); continue; };
                        if (Map.IsOcean(i + 1, j) && Map.IsOcean(i - 1, j)) { b.Die(); break; };*/
                        //внутренние углы angleinside
                        if (Map.IsBorderElement(i - 1, j, i, j - 1) && Map.IsOcean(i - 1, j - 1)) { b.SwitchType(DirectionType.Up, GroundType.angleinside); continue; };
                        if (Map.IsBorderElement(i - 1, j, i, j + 1) && Map.IsOcean(i - 1, j + 1)) { b.SwitchType(DirectionType.Right, GroundType.angleinside); continue; };
                        if (Map.IsBorderElement(i + 1, j, i, j - 1) && Map.IsOcean(i + 1, j - 1)) { b.SwitchType(DirectionType.Left, GroundType.angleinside); continue; };
                        if (Map.IsBorderElement(i + 1, j, i, j + 1) && Map.IsOcean(i + 1, j + 1)) { b.SwitchType(DirectionType.Down, GroundType.angleinside); continue; };
                        //внешние углы
                        if (Map.IsOcean(i - 1, j) && Map.IsOcean(i, j - 1)) { b.SwitchType(DirectionType.Up, GroundType.angle); continue; };
                        if (Map.IsOcean(i - 1, j) && Map.IsOcean(i, j + 1)) { b.SwitchType(DirectionType.Right, GroundType.angle); continue; };
                        if (Map.IsOcean(i + 1, j) && Map.IsOcean(i, j - 1)) { b.SwitchType(DirectionType.Left, GroundType.angle); continue; };
                        if (Map.IsOcean(i + 1, j) && Map.IsOcean(i, j + 1)) { b.SwitchType(DirectionType.Down, GroundType.angle); continue; };
                        //прямые границы
                        if (Map.IsOcean(i - 1, j)) { b.SwitchType(DirectionType.Up, GroundType.border); continue; };
                        if (Map.IsOcean(i + 1, j)) { b.SwitchType(DirectionType.Down, GroundType.border); continue; };
                        if (Map.IsOcean(i, j - 1)) { b.SwitchType(DirectionType.Left, GroundType.border); continue; };
                        if (Map.IsOcean(i, j + 1)) { b.SwitchType(DirectionType.Right, GroundType.border); continue; };
                    }
                }
            }
        }

        internal static bool IsOcean(this Map Map, int i, int j) => Map[i, j] is Ocean;

        internal static bool IsBorderElement(this Map Map, int i, int j, int x, int y) => Map[i, j] is Ground && Map[x, y] is Ground;
    }
}

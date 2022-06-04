using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace Game60s.Model
{
    internal class Map : IEnumerable
    {
        internal static AEntity[][] Mapp;
        internal int LengthX { get => Mapp.Length; }
        internal int LengthY { get => Mapp.Length; }

        public static Func<int, int, bool> IsWithinMap = (i, j) => i > -1 && j > -1 && i < Mapp.Length && j < Mapp.Length;

        internal Map(int size)
        {
            Mapp = new AEntity[size][];
            for (int i = 0; i < Mapp.Length; i++)
                Mapp[i] = new AEntity[size];
        }

        // не трогай, так НАДА
        public AEntity GetItemPoCoordinate(int x, int y) =>
            Mapp[y / GameModell.ElementSize][x / GameModell.ElementSize];

        // Это тоже
        public AEntity GetItemPoCoordinate(Point p) =>
            this[p.Y / GameModell.ElementSize, p.X / GameModell.ElementSize];

        internal Map(AEntity[][] mapCells) => Mapp = mapCells;

        internal AEntity this[int i, int j]
        {
            get
            {
                return IsWithinMap(i, j) ? Mapp[i][j] :
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

        public string[,] GetStringMap()
        {
            var res = new string[Mapp.Length, Mapp.Length];
            for (int i = 0; i < Mapp.Length; i++)
            {
                for (int j = 0; j < Mapp.Length; j++)
                {
                    res[i, j] = Mapp[i][j].ToString();
                }
            }
            return res;
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
                        //
                        if (Map.IsOcean(i, j - 1) && Map.IsOcean(i, j + 1)) { b.Die(); continue; }
                        if (Map.IsOcean(i - 1, j) && Map.IsOcean(i + 1, j)) { b.Die(); continue; }
                    }
                }
            }
        }

        internal static bool IsOcean(this Map Map, int i, int j) => Map[i, j] is Ocean;

        internal static bool IsBorderElement(this Map Map, int i, int j, int x, int y) => Map[i, j] is Ground && Map[x, y] is Ground;

        internal static bool IsOnDirt(this System.Drawing.Point pos) =>
            GameModell.Map[pos.X / GameModell.ElementSize, pos.Y / GameModell.ElementSize] is Ground;    
    }
}

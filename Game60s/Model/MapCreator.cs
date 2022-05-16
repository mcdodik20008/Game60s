using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TinkerWorX.AccidentalNoiseLibrary;

namespace Game60s.Model
{
    internal static class MapCreator
    {
        /// <summary>
        /// Создает карту из символьного представления в файле. Использует доп методы.
        /// </summary>
        /// <returns>Карта из AEntity</returns>
        internal static Map Create()
        {
            string FileNameOrPath = @"..\..\Model\Map.txt";
            return new Map(
                GetMapIEntity(
                    GetMapChar(
                        File.ReadAllLines(FileNameOrPath)
                    )
                )
             );
        }

        /// <summary>
        /// Преобразует строки файла в двумерный список символов.
        /// </summary>
        /// <param name="stringMap"></param>
        /// <returns></returns>
        internal static List<List<char>> GetMapChar(string[] stringMap) =>
            stringMap.Select(x => x.Select(y => y).ToList()).ToList();

        internal static Dictionary<char, Func<int, int, AEntity>> charToIEntity = new Dictionary<char, Func<int, int, AEntity>>()
        {
            ['O'] = Ocean.Create,
            ['B'] = Ground.Create
        };

        /// <summary>
        /// Преобразует двумерный список символов в двумерный массим объектов карты.
        /// </summary>
        /// <param name="charCell"></param>
        /// <returns>AEntity[][]</returns>
        internal static AEntity[][] GetMapIEntity(List<List<char>> charCell)
        {
            var map = new AEntity[charCell.Count][];
            var row = new AEntity[charCell.Count];
            for (int x = 0; x < charCell.Count; x++)
            {
                for (int y = 0; y < charCell[x].Count; y++)
                    row[y] = charToIEntity[charCell[x][y]](x * GameModell.ElementSize, y * GameModell.ElementSize);
                map[x] = row.Clone() as AEntity[];
            }
            return map;
        }

        internal static void SetMapHeight(this Map map)
        {
            var rand = new Random();
            var center = (rand.Next(8, 10), rand.Next(8, 10));
            ImplicitFractal HeightMap = new ImplicitFractal(FractalType.Multi,
                                                            BasisType.Simplex,
                                                            InterpolationType.Quintic);
            HeightMap.Frequency = 0.05;
            HeightMap.Octaves = 3;

            var CalculatedHeightMap = new int[map.LengthX, map.LengthY];

            var mapMin = int.MaxValue;
            var mapMax = int.MinValue;

            var mapHeight = "";

            for (int x = 0; x < map.LengthX; x++)
            {
                for (int y = 0; y < map.LengthY; y++)
                {
                    int d = (int)Math.Floor(Math.Sqrt((center.Item1 - x) * (center.Item1 - x) + (center.Item2 - y) * (center.Item2 - y)));
                    var calculatedHeight = (int)(Math.Abs(HeightMap.Get(x, y) * (1 - d * HeightMap.Frequency) * 10));
                    if (calculatedHeight < mapMin)
                        mapMin = calculatedHeight;
                    if (calculatedHeight > mapMax)
                        mapMax = calculatedHeight;
                    CalculatedHeightMap[x, y] = calculatedHeight;
                }
            }

            for (int x = 0; x < map.LengthX; x++)
            {
                for (int y = 0; y < map.LengthY; y++)
                {
                    CalculatedHeightMap[x, y] -= mapMin;
                    mapHeight += $"{CalculatedHeightMap[x, y]} ";
                }
                mapHeight += "\n";
            }

            MessageBox.Show(mapHeight);
            mapHeight = "";

            for (int x = 0; x < map.LengthX; x++)
            {
                for (int y = 0; y < map.LengthY; y++)
                {
                    int left = (x - 1 >= 0) ? CalculatedHeightMap[x - 1, y] : 0;
                    int right = (x + 1 <= map.LengthX - 1) ? CalculatedHeightMap[x + 1, y] : 0;
                    int up = (y - 1 >= 0) ? CalculatedHeightMap[x, y - 1] : 0;
                    int down = (y + 1 <= map.LengthX - 1) ? CalculatedHeightMap[x, y + 1] : 0;

                    if ((left + right + up + down) / 4 < CalculatedHeightMap[x, y])
                        CalculatedHeightMap[x, y] = (left + right + up + down) / 4;

                    if ((up + down) / 2 - CalculatedHeightMap[x, y] > 1 && (left + right) / 2 - CalculatedHeightMap[x, y] > 1)
                        CalculatedHeightMap[x, y] = (left + right + up + down) / 4;

                    if (up == down && up != CalculatedHeightMap[x, y])
                    {
                        if (left == right && left != CalculatedHeightMap[x, y])
                            CalculatedHeightMap[x, y] = left;
                        else
                            CalculatedHeightMap[x, y] = up;
                        continue;
                    }

                    if (left + right > CalculatedHeightMap[x, y] * 2)
                        if ((left + right) / 2 < left && (left + right) / 2 < right)
                            CalculatedHeightMap[x, y] = (left + up) / 2;

                    if (up + down > CalculatedHeightMap[x, y] * 2)
                        if ((up + down) / 2 < up && (up + down) / 2 < down)
                            CalculatedHeightMap[x, y] = (up + down) / 2;

                }
            }

            for (int x = 0; x < map.LengthX; x++)
            {
                for (int y = 0; y < map.LengthY; y++)
                {
                    int left = (x - 1 >= 0) ? CalculatedHeightMap[x - 1, y] : 0;
                    int right = (x + 1 <= map.LengthX - 1) ? CalculatedHeightMap[x + 1, y] : 0;
                    int up = (y - 1 >= 0) ? CalculatedHeightMap[x, y - 1] : 0;
                    int down = (y + 1 <= map.LengthX - 1) ? CalculatedHeightMap[x, y + 1] : 0;

                    if (up + down < CalculatedHeightMap[x, y])
                    {
                        if (left + right < CalculatedHeightMap[x, y])
                            CalculatedHeightMap[x, y] = (left + right) / 2;
                        else
                            CalculatedHeightMap[x, y] = (up + down) / 2;
                    }
                }
            }

/*            for (int x = 0; x < map.LengthX; x++)
            {
                for (int y = 0; y < map.LengthY; y++)
                {
                    int left = (x - 1 >= 0) ? CalculatedHeightMap[x - 1, y] : 0;
                    int right = (x + 1 <= map.LengthX - 1) ? CalculatedHeightMap[x + 1, y] : 0;
                    int up = (y - 1 >= 0) ? CalculatedHeightMap[x, y - 1] : 0;
                    int down = (y + 1 <= map.LengthX - 1) ? CalculatedHeightMap[x, y + 1] : 0;

                    if (up + down < CalculatedHeightMap[x, y])
                    {
                        if (left + right < CalculatedHeightMap[x, y])
                            CalculatedHeightMap[x, y] = (left + right) / 2;
                        else
                            CalculatedHeightMap[x, y] = (up + down) / 2;
                    }
                }
            }
*/
            for (int x = 0; x < map.LengthX; x++)
            {
                for (int y = 0; y < map.LengthY; y++)
                {
                    (map[x, y] as IMapObject).Height = CalculatedHeightMap[x, y];
                    mapHeight += $"{CalculatedHeightMap[x, y]} ";
                }
                mapHeight += "\n";
            }

            MessageBox.Show(mapHeight);
        }
    }
}

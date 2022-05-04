using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
                    row[y] = charToIEntity[charCell[x][y]](x, y);
                map[x] = row.Clone() as AEntity[];
            }
            return map;
        }

        internal static void SetMapHeight(this Map map)
        {
            var rand = new Random();
            foreach (AEntity area in map)
            {
                var x = area.X;
                var y = area.Y;
                (area as IMapObject).Height = rand.Next(0, 10);
            }
        }
    }
}

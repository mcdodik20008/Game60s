using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Game60s.Model
{
    internal static class MapCreator
    {
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

        internal static List<List<char>> GetMapChar(string[] stringMap) =>
            stringMap.Select(x => x.Select(y => y).ToList()).ToList();

        internal static Dictionary<char, Func<int, int, AEntity>> charToIEntity = new Dictionary<char, Func<int, int, AEntity>>()
        {
            ['O'] = Ocean.Create,
            ['B'] = Border.Create
        };

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
    }
}

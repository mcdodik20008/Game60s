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

        internal static Dictionary<char, Func<int, int, IEntity>> charToIEntity = new Dictionary<char, Func<int, int, IEntity>>()
        {
            ['E'] = EndMap.Create,
            ['F'] = Floor.Create,
            ['W'] = Wall.Create
        };

        internal static IEntity[][] GetMapIEntity(List<List<char>> charCell)
        {
            var map = new IEntity[charCell.Count][];
            var row = new IEntity[charCell.Count];
            for (int x = 0; x < charCell.Count; x++)
            {
                for (int y = 0; y < charCell[x].Count; y++)
                    row[y] = charToIEntity[charCell[x][y]](x * GameModell.ElementSize, y * GameModell.ElementSize);
                map[x] = row.Clone() as IEntity[];
            }  
            return map;
        }
    }
}

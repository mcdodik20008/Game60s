using System;
using Game60s.Model;

namespace Game60s.Tests
{
	public static class LoadForTest
	{
        public static void SetGameModell(string[] strMap)
        {
            new GameModell(new Map(
                MapCreator.GetMapIEntity(
                    MapCreator.GetMapChar(
                        strMap
                    )
                )
             ));
        }

        internal static void SetHeigthIsLineToUp(Map map)
        {
            for (int i = 0; i < map.LengthX; i++)
                for (int j = 0; j < map.LengthX; j++)
                    (map[i, j] as Ground).Height = i;
        }
    }
}


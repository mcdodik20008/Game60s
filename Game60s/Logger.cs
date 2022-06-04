using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game60s.Model;


namespace Game60s
{
    internal static class Logger
    {
        public static void CreateNewLog(Map map, Resourse[] res, int waterLevel)
        {
            StreamWriter output = new StreamWriter($"serialize{waterLevel}.txt");

            for (int i = 0; i < map.LengthX; i++)
            {
                for (int j = 0; j < map.LengthY; j++)
                    if (map[i, j].ToString().Skip(1).First() != 'G')
                        output.Write(map[i, j] + ", ");
                output.WriteLine();
            }


            foreach (var item in res.Where(x => x != null).OrderBy(x => x.X + x.Y))
                output.Write(item + ", ");

            output.Close();
        }
    }
}

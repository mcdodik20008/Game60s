using System;
using System.Linq;

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
            Map.SwitchBorder();
        }

        //это уберется, когда ты добавишь высоты и затопления клеток.
        // Что нжно сделать. Добавить высоту клетки + поднимать "Уровень воды(тоже надо сделать)" в модели
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
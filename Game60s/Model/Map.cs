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
}

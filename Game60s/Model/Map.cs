using System;
using System.Collections;
using System.Collections.Generic;

namespace Game60s.Model
{
    internal class Map : IEnumerable
    {
        internal static IEntity[][] Mapp;
        internal int LengthX { get => Mapp.Length; }
        internal int LengthY { get => Mapp.Length; }

        public static Func<int, int, bool> IsWithinMap = (i, j) => i > -1 && j > -1 && i < Mapp.Length * GameModell.ElementSize && j < Mapp.Length * GameModell.ElementSize;

        internal Map(int size)
        {
            Mapp = new IEntity[size][];
            for (int i = 0; i < Mapp.Length; i++)
                Mapp[i] = new IEntity[size];
        }

        internal Map(IEntity[][] mapCells) => Mapp = mapCells;

        internal IEntity this[int i, int j]
        {
            get
            {
                return IsWithinMap(i ,j) ? Mapp[i][j] :
                    throw new IndexOutOfRangeException($"Ты еблан? Какой нахуй ты делаешь.");
            }
            set
            {
                Mapp[i][j] = IsWithinMap(i, j) ? value :
                    throw new IndexOutOfRangeException($"Ты еблан? Какой нахуй ты делаешь.");
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

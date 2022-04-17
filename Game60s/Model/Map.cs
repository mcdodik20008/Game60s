using System;
using System.Collections;
using System.Collections.Generic;

namespace Game60s.Model
{
    internal class Map : IEnumerable
    {
        internal IEntity[][] Mapp;
        internal int LengthX { get => Mapp.Length; }
        internal int LengthY { get => Mapp.Length; }

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
                if (i > -1 && j > -1 && i < Mapp.Length && j < Mapp[i].Length)
                    return Mapp[i][j];
                throw new IndexOutOfRangeException($"Ты еблан? Какой нахуй ты делаешь.");
            }
            set
            {
                if (i > -1 && j > -1 && i < Mapp.Length && j < Mapp[i].Length)
                    Mapp[i][j] = value;
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

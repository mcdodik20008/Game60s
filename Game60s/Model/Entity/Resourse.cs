using System;

namespace Game60s.Model
{
    internal abstract class Resourse : AEntity, IDisposable
    {
        public int amount = 0;

        // Объясню потом
        public void Dispose()
        {
            for (int i = 0; i < GameModell.Resourse.Length; i++)
                if (this == GameModell.Resourse[i])
                    GameModell.Resourse[i] = null;
        }
    }
}

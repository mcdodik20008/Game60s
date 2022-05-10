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

        public bool TryGetThis(Player p)
        {
            var f = Math.Abs(p.X - X) < 20 && Math.Abs(p.Y - Y) < 40;
            if (f)
                GetThis(p);
            return f;
        }

        public void GetThis(Player p)
        {
            p.IncrementResourse();
            Dispose();
        }
    }
}

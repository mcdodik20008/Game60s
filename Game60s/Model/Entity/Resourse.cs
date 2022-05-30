using System;
using System.Drawing;

namespace Game60s.Model
{
    internal abstract class Resourse : AEntity, IDisposable
    {
        public int amount = 0;
        public Size size = new Size(20, 20);
        
        public void Dispose()
        {
            for (int i = 0; i < GameModell.Resourse.Length; i++)
                if (this == GameModell.Resourse[i])
                    GameModell.Resourse[i] = null;
        }
    }
}

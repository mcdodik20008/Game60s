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

        public AEntity WhereINAXOJYS() =>
    GameModell.Map[X / GameModell.ElementSize, Y / GameModell.ElementSize];

        public AEntity WhereINAXOJYS(Point point) =>
GameModell.Map[point.X / GameModell.ElementSize, point.Y / GameModell.ElementSize];

        public AEntity WhereINAXOJYS(int x, int y) =>
GameModell.Map[x / GameModell.ElementSize, y / GameModell.ElementSize];
    }

    internal static class ResourseExt
    {
        public static void DieIfOnOcean(this Resourse res)
        {
            if (res.WhereINAXOJYS() is Ocean 
             || res.WhereINAXOJYS(res.X + res.size.Width, res.Y) is Ocean
             || res.WhereINAXOJYS(res.X , res.Y + res.size.Height) is Ocean
             || res.WhereINAXOJYS(res.X + res.size.Width, res.Y + res.size.Height) is Ocean)
                res.Dispose();  
        }
    }
}

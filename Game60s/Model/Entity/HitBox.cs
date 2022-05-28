using System.Drawing;

namespace Game60s.Model
{
    internal class HitBox
    {
        int x;
        int y;

        public HitBox(int X, int Y, Size s)
        {
            size = s;
            x = Y;
            y = X;
        }

        public Size size { get; set; }
        public Point LeftUp { get => new Point(x, y); }
        public Point LeftDown { get => new Point(x, y + size.Width); }
        public Point RightUp { get => new Point(x + size.Height, y); }
        public Point RightDown { get => new Point(x + size.Height, y + size.Width); }

        public void UpdateCoordinate(int X, int Y)
        {
            x = X;
            y = Y;
        }
    }

    internal static class HitBoxExt
    {
        public static bool IsOnOcean(this HitBox hB) =>
            GameModell.Map.GetItemPoCoordinate(hB.LeftDown) is Ocean ||
            GameModell.Map.GetItemPoCoordinate(hB.LeftUp) is Ocean ||
            GameModell.Map.GetItemPoCoordinate(hB.RightDown) is Ocean ||
            GameModell.Map.GetItemPoCoordinate(hB.RightUp) is Ocean; 
    }
}

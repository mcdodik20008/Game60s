using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

namespace Game60s.Model
{
    internal class EndMap : IEntity
    {
        public readonly int X;
        public readonly int Y;

        public EndMap(int x, int y)
        {
            X = x; Y = y;
        }

        public static IEntity Create(int x, int y) => new EndMap(x, y);
        public Vector2 Act(Keys key) => Vector2.Zero;
        public string GetNameImage() => "EndMap.png";
        public Point PositionOnMap() => new Point(X, Y);
    }
}

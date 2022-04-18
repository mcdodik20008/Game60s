using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Game60s.Model
{
    public class Floor : IEntity
    {
        public int X, Y;
        public Floor(int x, int y)
        {
            X = x; Y = y;
        }

        internal static IEntity Create(int x, int y) => new Floor(x, y);
        public string GetNameImage() => "Floor.png";
        public Point PositionOnMap() => new Point(X, Y);
        public void Act(HashSet<Keys> key) { }
    }
}

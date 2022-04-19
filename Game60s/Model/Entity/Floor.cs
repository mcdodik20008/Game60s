using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Game60s.Model
{
    internal class Floor : IEntity
    {
        public int X, Y;
        public Floor(int x, int y)
        {
            X = x; Y = y;
            Position = new Point(x, y);
        }

        internal static IEntity Create(int x, int y) => new Floor(x, y);
        public override string GetNameImage() => "Floor.png";
        public override void Act(HashSet<Keys> key) { }

    }
}

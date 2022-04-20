using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Game60s.Model
{
    internal class Wall : IEntity
    {
        public int X, Y;

        public Wall(int x, int y)
        {
            X = x; Y = y;
            Position = new Point(x, y);
        }


        public static IEntity Create(int x, int y) => new Wall(x, y);

        public override string GetNameImage() => "Wall.png";

        public override void Act(HashSet<Keys> key) { }

        public override IEntity Die() => EndMap.Create(X, Y);
    }
}

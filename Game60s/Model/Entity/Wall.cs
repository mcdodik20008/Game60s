using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game60s.Model
{
    internal class Wall : IEntity
    {
        int X, Y;

        public Wall(int x, int y)
        {
            X = x; Y = y;
        }

        public string GetNameImage() => "Wall.png";
        public static IEntity Create(int x, int y) => new Wall(x, y);

        public Point PositionOnMap() => new Point(X, Y);

        public void Act(HashSet<Keys> key) { }
    }
}

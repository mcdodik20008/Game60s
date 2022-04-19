using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

namespace Game60s.Model
{
    internal class EndMap : IEntity
    {
        public EndMap(int x, int y)
        {
            X = x; Y = y;
            Position = new Point(x, y);
        }

        public static IEntity Create(int x, int y) => new EndMap(x, y);

        public override void Act(HashSet<Keys> key) { }

        public override string GetNameImage() => "EndMap.png";
    }
}

using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Game60s.Model
{
    internal class Border : AEntity
    {
        public int X, Y;
        public DirectionType Direction { get; set; }

        public Border(int x, int y)
        {
            X = x; Y = y;
            Position = new Point(x * GameModell.ElementSize, y * GameModell.ElementSize);
        }


        public static AEntity Create(int x, int y) => new Border(x, y);

        public override string GetNameImage() => "border.png";

        public override void Act(HashSet<Keys> key) { }

        public override AEntity Die() => Ocean.Create(X, Y);
    }
}

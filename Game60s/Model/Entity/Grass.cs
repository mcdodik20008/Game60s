using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Game60s.Model
{
    internal class Grass : AEntity
    {
        public int X, Y;
        public Grass(int x, int y)
        {
            X = x; Y = y;
            Position = new Point(x * GameModell.ElementSize, y * GameModell.ElementSize);
        }

        internal static AEntity Create(int x, int y) => new Grass(x, y);
        public override string GetNameImage() => "grass.png";
        public override void Act(HashSet<Keys> key) { }

        public override AEntity Die() => Ocean.Create(X, Y);
    }
}

using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Game60s.Model
{
    internal class Grass : IEntity
    {
        public int X, Y;
        public Grass(int x, int y)
        {
            X = x; Y = y;
            Position = new Point(x * GameModell.ElementSize, y * GameModell.ElementSize);
        }

        internal static IEntity Create(int x, int y) => new Grass(x, y);
        public override string GetNameImage() => "grass.png";
        public override void Act(HashSet<Keys> key) { }

        public override IEntity Die() => Ocean.Create(X, Y);
    }
}

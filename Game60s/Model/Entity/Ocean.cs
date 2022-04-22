using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

namespace Game60s.Model
{
    internal class Ocean : AEntity
    {
        public Ocean(int x, int y)
        {
            X = x; Y = y;
            Position = new Point(x * GameModell.ElementSize, y * GameModell.ElementSize);
        }

        public static AEntity Create(int x, int y) => new Ocean(x, y);

        public override void Act(HashSet<Keys> key) { }

        public override string GetNameImage() => "ocean.png";

        public override AEntity Die() => this;
    }
}

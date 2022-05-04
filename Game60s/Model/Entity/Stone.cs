using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Game60s.Model
{
    internal class Stone : Resourse
    {
        public Stone(int x, int y)
        {
            X = x; Y = y;
            PositionOnForm = new Point(x, y);
        }

        public override void Act(HashSet<Keys> key) { }

        public override AEntity Die() => this;
    }
}

using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Game60s.Model
{
    internal class Sticks : Resourse
    {
        public Sticks(int x, int y)
        {
            X = x; Y = y;
            Position = new Point(x, y);
        }

        public override void Act(HashSet<Keys> key) { }

        public override IEntity Die()
        {
            throw new System.NotImplementedException();
        }

        public override string GetNameImage() => "sticks.png";
    }
}

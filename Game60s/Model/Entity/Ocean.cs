using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Game60s.Model
{
    internal class Ocean : AEntity, IMapObject
    {
        public int Height { get; set; }

        public Ocean(int x, int y)
        {
            X = x; Y = y;
        }

        public static AEntity Create(int x, int y) => new Ocean(x, y);

        public override void Act(HashSet<Keys> key) { }

        public override AEntity Die() => this;
    }
}

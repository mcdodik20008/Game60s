using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

namespace Game60s.Model
{
    internal abstract class Resourse : IEntity
    {
        public int amount = 0;
        protected int X, Y;

        public abstract string GetNameImage();

        public void Act(HashSet<Keys> key) { }

        public Point PositionOnMap() => new Point(X, Y);
    }
}

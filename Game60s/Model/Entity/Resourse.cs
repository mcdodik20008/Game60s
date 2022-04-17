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

        public Vector2 Act(Keys key) => Vector2.Zero;

        public Point PositionOnMap() => new Point(X, Y);
    }
}

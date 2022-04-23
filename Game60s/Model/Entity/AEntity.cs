using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Game60s.Model
{
    internal abstract class AEntity
    {
        public int X, Y;
        public int Hp = 3;
        public Point Position { get; set; }
        public abstract string GetNameImage();
        public abstract void Act(HashSet<Keys> key);
        public abstract AEntity Die();
    }
}

using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Game60s.Model
{
    internal abstract class IEntity
    {
        public int X, Y;
        public int Hp = 5;
        public Point Position { get; set; }
        public abstract string GetNameImage();
        public abstract void Act(HashSet<Keys> key);
        public abstract IEntity Die();
    }
}

using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Game60s.Model
{
    internal class Raft : AEntity
    {
        private static Point delta = new Point(50, 100);
        private static int step = 2;

        //пока так проще
        public Raft(Player p)
        {
            p.OnRaft = true;
            X = p.X - delta.X;
            Y = p.Y - delta.Y;
        }

        public Raft(Babuin b)
        {
            b.OnRaft = true;
            X = b.X - delta.X;
            Y = b.Y - delta.Y;
        }

        public override void Act(HashSet<Keys> keys)
        {
            //Добавь ограничения на ходьбу
            step = keys.Contains(Keys.ShiftKey) ? 4 : 2;
            step = keys.Contains(Keys.ControlKey) ? 1 : 2;
            if (keys.Contains(Keys.Left))
                X -= step;
            if (keys.Contains(Keys.Right))
                X += step;
            if (keys.Contains(Keys.Up))
                Y -= step;
            if (keys.Contains(Keys.Down))
                Y += step;
        }

        public override AEntity Die() => this;
    }
}

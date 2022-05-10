using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Game60s.Model
{
    internal class Raft : AEntity
    {
        private static Size delta = new Size(50, 100);
        private static int step = 2;

        public Raft(Player player)
        {
            player.OnRaft = true;
            PositionOnForm = player.PositionOnForm - delta;
            X = PositionOnForm.X;
            Y = PositionOnForm.Y;
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

            PositionOnForm = new Point(X, Y);
        }

        public override AEntity Die() => this;
    }
}

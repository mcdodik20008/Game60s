using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Game60s.Model
{
    internal class Player : AEntity
    {
        Size sizePng = new Size(28, 46);
        int step = 2;
        public List<Resourse> Resourses = new List<Resourse>();
        public bool OnRaft = false;
        public int CountResourse { get; private set; }
        public void IncrementResourse() => CountResourse++;

        public Player(int x, int y)
        {
            X = x; Y = y;
        }

        public override void Act(HashSet<Keys> keys)
        {
            //Добавь ограничения на ходьбу
            if (!OnRaft)
            {
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
            else
                this.ActOnRaft();
            PositionOnForm = new Point(X, Y);
        }

        public override AEntity Die() => this;
    }
}

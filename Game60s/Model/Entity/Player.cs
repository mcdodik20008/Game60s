using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Game60s.Model
{
    internal class Player : AEntity, ICanCollect, ICanUseRaft
    {
        Size sizePng = new Size(28, 46);
        int step = 2;
        public List<Resourse> Resourses = new List<Resourse>();
        public bool OnRaft { get; set; }
        public int CountResourse { get; set; }
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
                if (keys.Contains(Keys.ShiftKey))
                    step = 3;
                else if (keys.Contains(Keys.ControlKey))
                    step = 1;
                else
                    step = 2;

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
        }

        public void TryGetThis(Resourse[] resourses)
        {
            foreach (var p in resourses)
                if (p != null && Math.Abs(p.X - X) < 20 && Math.Abs(p.Y - Y) < 40)
                    GetThis(p);
        }

        public void GetThis(Resourse res)
        {
            this.IncrementResourse();
            res.Dispose();
        }
        public override AEntity Die() => this;
    }
}

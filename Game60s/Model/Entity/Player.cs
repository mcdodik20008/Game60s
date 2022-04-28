using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Game60s.Model
{
    internal class Player : AEntity
    {
        public int X, Y;
        Size sizePng = new Size(28, 46);
        int step = 2;
        public List<Resourse> Resourses = new List<Resourse>();

        public Player(int x, int y)
        {
            X = x; Y = y;
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

        public override string GetNameImage() => "player.png";

        public override AEntity Die() => this;
    }
}

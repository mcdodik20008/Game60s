using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Game60s.Model
{
    internal class Player : AEntity
    {
        Size sizePng = new Size(28, 46);
        public int X;
        public int Y;
        int step = 2;
        public Player(int x, int y)
        {
            X = x; Y = y;
        }

        public override void Act(HashSet<Keys> keys)
        {
            //пофиксить
            if (keys.Contains(Keys.Left))
                X -= step;
            if (keys.Contains(Keys.Right))
                X += step;
            if (keys.Contains(Keys.Up))
                Y -= step;
            if (keys.Contains(Keys.Down))
                Y += step;

            Position = new Point(X, Y);
        }

        public override string GetNameImage() => "player.png";

        public override AEntity Die() => this;
    }
}

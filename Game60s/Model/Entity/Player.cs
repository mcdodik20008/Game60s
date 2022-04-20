using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Game60s.Model
{
    internal class Player : IEntity
    {
        public int X;
        public int Y;

        public Player(int x, int y)
        {
            X = x; Y = y;
        }

        public override void Act(HashSet<Keys> keys)
        {
            //пофиксить
            if (keys.Contains(Keys.Left) && Map.IsWithinMap(X, Y) && NewMethod(X + 1, Y) && NewMethod(X, Y + GameModell.ElementSize - 1))
                X -= 1;
            if (keys.Contains(Keys.Right) && Map.IsWithinMap(X, Y) && NewMethod(X + GameModell.ElementSize, Y) && NewMethod(X + GameModell.ElementSize, Y + GameModell.ElementSize - 5))
                X += 1;
            if (keys.Contains(Keys.Up) && Map.IsWithinMap(X, Y) && NewMethod(X, Y - 1) && NewMethod(X + GameModell.ElementSize - 5, Y - 1))
                Y -= 1;
            if (keys.Contains(Keys.Down) && Map.IsWithinMap(X, Y) && NewMethod(X, Y + GameModell.ElementSize) && NewMethod(X + GameModell.ElementSize - 5, Y + GameModell.ElementSize))
                Y += 1;

            Position = new Point(X, Y);
        }

        private bool NewMethod(int x, int y)
        {
            return GameModell.Map[y / GameModell.ElementSize, x / GameModell.ElementSize] is Floor;
        }

        public override string GetNameImage() => "Player.png";

        public override IEntity Die() => this;
    }
}

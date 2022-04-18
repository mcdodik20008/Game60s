using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Game60s.Model
{
    public class Player : IEntity
    {
        public int X;
        public int Y;

        public Player(int x, int y)
        {
            X = x; Y = y;
        }

        public void Act(HashSet<Keys> keys)
        {
            if (keys.Contains(Keys.Left) && Map.IsWithinMap(X, Y) && GameModell.Map[Y / GameModell.ElementSize, (X - 1) / GameModell.ElementSize] is Floor)
                X -= 1;
            if (keys.Contains(Keys.Right) && Map.IsWithinMap(X, Y) && GameModell.Map[Y / GameModell.ElementSize, X / GameModell.ElementSize+1] is Floor)
                X += 1;
            if (keys.Contains(Keys.Up) && Map.IsWithinMap(X, Y) && GameModell.Map[(Y - 1) / GameModell.ElementSize, X / GameModell.ElementSize] is Floor)
                Y -= 1;
            if (keys.Contains(Keys.Down) && Map.IsWithinMap(X, Y) && GameModell.Map[Y / GameModell.ElementSize+1, X / GameModell.ElementSize] is Floor)
                Y += 1;
        }

        public string GetNameImage() => "Player.png";

        public Point PositionOnMap() => new Point(X, Y);
    }
}

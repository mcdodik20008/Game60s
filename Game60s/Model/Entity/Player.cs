using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
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

        public Vector2 Act(HashSet<Keys> keys)
        {
            Vector2 vector2 = new Vector2(0, 0);
            if (keys.Contains(Keys.Up) && Map.IsWithinMap(X + (int)vector2.X, Y + (int)vector2.Y))
                X -= 1;
            if (keys.Contains(Keys.Down) && Map.IsWithinMap(X + (int)vector2.X, Y + (int)vector2.Y))
                X += 1;
            if (keys.Contains(Keys.Left) && Map.IsWithinMap(X + (int)vector2.X, Y + (int)vector2.Y))
                Y -= 1;
            if (keys.Contains(Keys.Right)&& Map.IsWithinMap(X + (int)vector2.X, Y + (int)vector2.Y))
                Y += 1;
            if (Map.IsWithinMap(X + (int)vector2.X, Y + (int)vector2.Y))
                return Vector2.Zero;
            return vector2;
        }

        public string GetNameImage() => "Player.png";

        public Point PositionOnMap() => new Point(X, Y);
    }
}

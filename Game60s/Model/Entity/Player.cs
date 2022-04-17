using System.Drawing;
using System.Numerics;
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

        public Vector2 Act(Keys key)
        {
            switch (key)
            {
                case Keys.Up:
                    if (X - 1 < GameModell.Map.LengthY)
                        return new Vector2(-1, 0);
                    break;
                case Keys.Down:
                    if (X + 1 >= 0)
                        return new Vector2(1, 0);
                    break;
                case Keys.Right:
                    if (Y + 1 < GameModell.Map.LengthY)
                        return new Vector2(0, 1);
                    break;
                case Keys.Left:
                    if (Y - 1 >= 0)
                        return new Vector2(0, -1);
                    break;
            }
            return Vector2.Zero;
        }

        public string GetNameImage() => "Player.png";

        public Point PositionOnMap() => new Point(X, Y);
    }
}

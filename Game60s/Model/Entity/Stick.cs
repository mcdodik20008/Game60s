using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Game60s.Model
{
    internal class Stick : Resourse
    {
        public Stick(int x, int y)
        {
            X = x; Y = y;
        }

        public Stick(Point pos)
        {
            X = pos.X; Y = pos.Y;
        }

        public static Bitmap GetImage { get { return Images.sticks; } }

        public override void Act(HashSet<Keys> key) { }

        public override AEntity Die() => this;

        public static Stick CreateRandomXY()
        {
            return new Stick(SetRandomCoordinateOnDirt());
        }

        private static Point SetRandomCoordinateOnDirt()
        {
            var pos = new Point(-100, -100);
            while (!pos.IsOnDirt())
            {
                pos.X = GameModell.Rnd.Next(GameModell.ElementSize, GameModell.Map.LengthX * (GameModell.ElementSize - 3));
                pos.Y = GameModell.Rnd.Next(GameModell.ElementSize, GameModell.Map.LengthX * (GameModell.ElementSize - 3));
            }
            return pos;
        }
    }
}

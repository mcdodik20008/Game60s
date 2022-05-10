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
            PositionOnForm = new Point(x, y);
        }

        public static Bitmap GetImage { get { return Images.sticks; } }

        public override void Act(HashSet<Keys> key) { }

        public override AEntity Die() => this;

        public static Stick CreateRandomXY()
        {
            return new Stick
                (
                GameModell.Rnd.Next(GameModell.ElementSize, GameModell.Map.LengthX * (GameModell.ElementSize - 1)),
                GameModell.Rnd.Next(GameModell.ElementSize, GameModell.Map.LengthX * (GameModell.ElementSize - 1))
                );
        }
    }
}

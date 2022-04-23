using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Game60s.Model
{
    internal class Border : AEntity, IBorderElement
    {
        public int X, Y;
        DirectionType direction;
        bool isRotated = true;
        public BorderType borderType = BorderType.grass;

        Dictionary<BorderType, string> borderTypeToNameImage = new Dictionary<BorderType, string>()
        {
            [BorderType.border] = "border.png",
            [BorderType.angleInside] = "angle_inside.png",
            [BorderType.angle] = "angle.png",
            [BorderType.grass] = "grass.png"
        };

        public Border(int x, int y)
        {
            X = x; Y = y;
            Position = new Point(x * GameModell.ElementSize, y * GameModell.ElementSize);
        }

        public static AEntity Create(int x, int y) => new Border(x, y);

        public override string GetNameImage() => borderTypeToNameImage[borderType];

        public override void Act(HashSet<Keys> key) { }

        public override AEntity Die() => Ocean.Create(X, Y);

        public DirectionType GetDirection()
        {
            return direction;
        }

        public void SetDirection(DirectionType direction)
        {
            isRotated = false;
            this.direction = direction;
        }

        public bool NeedTurn()
        {
            return isRotated;
        }

        public void SetBorderType(BorderType bT = BorderType.grass)
        {
            borderType = bT;
        }
    }
}

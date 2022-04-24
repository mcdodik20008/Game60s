using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Game60s.Model
{
    //сделал для определения вида "пляжа" и/или угла
    public enum BorderType
    {
        grass, border, angle, angleInside
    }

    //Добавил зависимость типа границы и пнг файла. Сделал направление стены и была развернута в правильное направление или нет.
    internal class Border : AEntity
    {
        public int X, Y;
        //может как-то переименовать.
        public bool WasRotated = true;

        DirectionType direction;
        public DirectionType Direction
        {
            get => direction;

            set
            {
                WasRotated = false;
                direction = value;
            }
        }

        public BorderType BorderType { get; set; }

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
            PositionOnForm = new Point(x * GameModell.ElementSize, y * GameModell.ElementSize);
        }

        public static AEntity Create(int x, int y) => new Border(x, y);

        public override string GetNameImage() => borderTypeToNameImage[BorderType];

        public override void Act(HashSet<Keys> key) { }

        public override AEntity Die() => Ocean.Create(X, Y);
    }
}

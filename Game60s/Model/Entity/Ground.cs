using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Game60s.Model
{
    //сделал для определения вида "пляжа" и/или угла
    public enum GroundType
    {
        grass, border, angle, angleinside
    }

    //Добавил зависимость типа границы и пнг файла. Сделал направление стены и была развернута в правильное направление или нет.
    internal class Ground : AEntity, IMapObject
    {
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

        public GroundType GroundType { get; set; }
        public int Height { get; set; }

        public Ground(int x, int y)
        {
            X = x; Y = y;
        }

        public static AEntity Create(int x, int y) => new Ground(x, y);

        public override void Act(HashSet<Keys> key) { }

        public override AEntity Die() => Ocean.Create(X, Y);
    }
}

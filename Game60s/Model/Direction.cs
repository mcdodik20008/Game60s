using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game60s.Model
{
    public static class Direction
    {
        public static double ConvertDirectionToAngle(this DirectionType dT)
        {
            switch (dT)
            {
                case DirectionType.Up: return 0;
                case DirectionType.Right: return 90;
                case DirectionType.Down: return 180;
                case DirectionType.Left: return 240;
                default: throw new ArgumentException();
            }
        }

        public static double ConvertDirectionToRadian(this DirectionType dT)
        {
            switch (dT)
            {
                case DirectionType.Up: return 0;
                case DirectionType.Right: return -Math.PI / 2;
                case DirectionType.Down: return Math.PI;
                case DirectionType.Left: return Math.PI / 2;
                default: throw new ArgumentException();
            }
        }
    }

    public enum DirectionType
    {
        Up, Down, Left, Right
    }
}

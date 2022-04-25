using System;
using System.Drawing;
using System.Collections;

namespace Game60s.Model
{
    //тут добавил понятно что
    public enum DirectionType
    {
        Up, Down, Left, Right
    }


    public static class Direction 
    {
        //тут понятно
        public static float ConvertDirectionToAngle(this DirectionType dT)
        {
            switch (dT)
            {
                case DirectionType.Up: return 0;
                case DirectionType.Right: return 90;
                case DirectionType.Down: return 180;
                case DirectionType.Left: return 270;
                default: throw new ArgumentException();
            }
        }

        //для поворота картины
        public static Image RotateImage(this Bitmap img, float rotationAngle)
        {
            Bitmap bmp = new Bitmap(GameModell.ElementSize, GameModell.ElementSize);
            Graphics g = Graphics.FromImage(bmp);
            g.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);
            g.RotateTransform(rotationAngle);
            g.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);
            g.DrawImage(img, new Point(0, 0));
            g.Dispose();
            return bmp;
        }

        internal static void SwitchType(this Border b, DirectionType direction, BorderType type)
        {
            b.Direction = direction;
            b.BorderType = type;
        }
    }
}

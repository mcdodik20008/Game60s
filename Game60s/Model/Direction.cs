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
        /// <summary>
        /// Преобразует направление в угол
        /// </summary>
        /// <param name="dT"></param>
        /// <returns>Rotation angle</returns>
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
        /// <summary>
        /// Создает новую, повернутую на rotationAngle градусов картинку.
        /// Важно: занимает до 1гб оперативы и во время сборки мусора лагает на слабом пк.
        /// Утечка памяти сдесь. переписать, чтоб запускалось только при изменении. Может сделать двумерный массив bitmap?
        /// </summary>
        /// <param name="img"></param>
        /// <param name="rotationAngle"></param>
        /// <returns>Image</returns>
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

        /// <summary>
        /// Меняет параметры переданного Border
        /// </summary>
        /// <param name="b"></param>
        /// <param name="direction"></param>
        /// <param name="type"></param>
        internal static void SwitchType(this Border b, DirectionType direction, BorderType type)
        {
            b.Direction = direction;
            b.BorderType = type;
        }
    }
}

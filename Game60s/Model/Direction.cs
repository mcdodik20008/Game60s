using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game60s.Model
{
    public static class Direction
    {
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

        public static Image RotateImage(this Bitmap img, float rotationAngle)
        {
            //create an empty Bitmap image
            Bitmap bmp = new Bitmap(GameModell.ElementSize, GameModell.ElementSize);

            //turn the Bitmap into a Graphics object
            Graphics gfx = Graphics.FromImage(bmp);

            //now we set the rotation point to the center of our image
            gfx.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);

            //now rotate the image
            gfx.RotateTransform(rotationAngle);
            gfx.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);

            //set the InterpolationMode to HighQualityBicubic so to ensure a high
            //quality image once it is transformed to the specified size
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //now draw our new image onto the graphics object
            gfx.DrawImage(img, new Point(0, 0));

            //dispose of our Graphics object
            gfx.Dispose();

            return bmp; //return the image
        }
    }

    public enum DirectionType
    {
        Up, Down, Left, Right
    }
}

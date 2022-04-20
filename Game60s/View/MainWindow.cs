using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Game60s.Controller;
using Game60s.Model;

namespace Game60s.Viev
{
    internal class MainWindow : Form
    {
        // проси меня что-то добавить в контроллер, пользуйся в основном контроллером, неймспейс я подрубил.
        // время до катастрофы из гей моледи
        private static int n = 1;
        private int timerTick = 0;
        
        public const int SizeVisibleMap = 12;
        public static Timer timer = new Timer();
        private readonly DirectoryInfo imagesDirectory = null;
        private readonly Dictionary<string, Bitmap> bitmaps = new Dictionary<string, Bitmap>();
        internal MainWindow()
        {
            DoubleBuffered = true;
            if (imagesDirectory == null)
                imagesDirectory = new DirectoryInfo(@"..\..\Model\Images");
            foreach (var e in imagesDirectory.GetFiles("*.png"))
                bitmaps[e.Name] = (Bitmap)Image.FromFile(e.FullName);

            timer.Interval = 1;
            timer.Tick += TimerTick;
            timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            ControllerWindow.EterationGameModel(timerTick);
            Invalidate();
            timerTick++;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(
                Brushes.Black, 0, GameModell.ElementSize, GameModell.ElementSize * GameModell.Map.LengthX,
                GameModell.ElementSize * GameModell.Map.LengthY);
            var Position = new Point(0, 0);
            for (int y = 0; y < SizeVisibleMap; y++)
            {
                for (int x = 0; x < SizeVisibleMap; x++)
                {
                    e.Graphics.DrawImage(bitmaps[GameModell.Map[x, y].GetNameImage()], GameModell.Map[y, x].Position);
                    Position = new Point(Position.X + GameModell.ElementSize, Position.Y);
                }
                Position = new Point(0, Position.Y + GameModell.ElementSize);
            }
            e.Graphics.DrawImage(bitmaps[GameModell.player.GetNameImage()], new Point(
                GameModell.player.Position.X, 
                GameModell.player.Position.Y));
        }
        protected override void OnKeyDown(KeyEventArgs e) => ControllerWindow.SetPressedKey(e.KeyCode);
        protected override void OnKeyUp(KeyEventArgs e) => ControllerWindow.RemoveKey(e.KeyCode);
    }
}
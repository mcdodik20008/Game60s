using Game60s.Controller;
using Game60s.Model;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Game60s.Viev
{
    internal class MainWindow : Form
    {
        // проси меня что-то добавить в контроллер, пользуйся в основном контроллером, неймспейс я подрубил.
        // время до катастрофы из гей моледи
        private int timerTick = 0;
        public const int SizeVisibleMap = 12;
        public static Timer timer = new Timer();

        internal MainWindow()
        {
            DoubleBuffered = true;
            timer.Interval = 1;
            timer.Tick += TimerTick;
            timer.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            for (int y = 0; y < SizeVisibleMap; y++)
                for (int x = 0; x < SizeVisibleMap; x++)
                    e.Graphics.DrawImage(GameModell.Map[x, y].GetImage(), GameModell.Map[y, x].PositionOnFormPoint);


            foreach (var item in GameModell.Resourse)
                if (item != null)
                    e.Graphics.DrawImage(Stick.GetImage, item.PositionOnFormPoint);

            if (GameModell.Raft != null)
                e.Graphics.DrawImage(GameModell.Raft.GetImage(), GameModell.Raft.PositionOnFormPoint);

            e.Graphics.DrawImage(GameModell.player.GetImage(), GameModell.player.PositionOnFormPoint);

            e.Graphics.DrawString(GameModell.player.CountResourse + @"/" + GameModell.ResoutseToRaft, new Font("Arial", 32), Brushes.Gray, 0, GameModell.ElementSize / 3);

            e.Graphics.DrawImage(GameModell.Babuin.GetImage(), GameModell.Babuin.PositionOnFormPoint);
            //e.Graphics.DrawImage(GameModell.Babuin2.GetImage(), GameModell.Babuin2.PositionOnFormPoint);
        }

        protected override void OnKeyDown(KeyEventArgs e) => ControllerWindow.SetPressedKey(e.KeyCode);

        protected override void OnKeyUp(KeyEventArgs e) => ControllerWindow.RemoveKey(e.KeyCode);

        private void TimerTick(object sender, EventArgs e)
        {
            ControllerWindow.EterationGameModel(timerTick);
            Invalidate();
            timerTick++;
        }
    }
}
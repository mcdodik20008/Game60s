using Game60s.Controller;
using Game60s.Model;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Game60s.Viev
{
    internal class MainWindow : Form
    {
        private int timerTick = 0;
        public const int SizeVisibleMap = 15;
        public static Timer timer = new Timer();

        internal MainWindow()
        {
            DoubleBuffered = true;
            timer.Interval = 10;
            timer.Tick += TimerTick;
            timer.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            switch (GameModell.GameState)
            {
                case GameModell.GameStates.GameStart:
                    WaitingScreen(e);
                    break;
                case GameModell.GameStates.GameProcess:
                    GameProcess(e);
                    break;
                case GameModell.GameStates.LevelWin:
                    PresKeyToNextLevel(e);
                    break;
                case GameModell.GameStates.LevelLose:
                    PresKeyToReloadGame(e);
                    break;
            }
        }

        private void PresKeyToReloadGame(PaintEventArgs e)
        {
            GameProcess(e);
            e.Graphics.DrawString($"Ты дошел до {GameModell.GameLevel} уровня.\n Макака оказалась сильнее чем ты \n не впадай в депрессию это всего лишь игра \n" +
                $"Нажм любую клавишу для перезапуска", new Font("Arial", 28), Brushes.Red, 50, 300);
        }

        private void PresKeyToNextLevel(PaintEventArgs e)
        {
            GameProcess(e);
            e.Graphics.DrawString($"Ты выйграл {GameModell.GameLevel} уровень.\nНажми на любую клавишу для продолжения", new Font("Arial", 28), Brushes.Red, 50, 300);
        }

        private void WaitingScreen(PaintEventArgs e)
        {
            if(timerTick / 30 % 2 == 0)
                e.Graphics.DrawImage(Images.WaitingScreen, 0, 0);
            else
                e.Graphics.DrawImage(Images.WaitingScreenAnim, 0, 0);
        }

        private void GameProcess(PaintEventArgs e)
        {
            for (int x = 0; x < SizeVisibleMap; x++)
                for (int y = 0; y < SizeVisibleMap; y++)
                {
                    e.Graphics.DrawImage(GameModell.Map[x, y].GetImage(), GameModell.Map[y, x].PositionOnFormPoint);
                    if (GameModell.WaterLine == (GameModell.Map[x, y] as IMapObject).Height)
                        e.Graphics.DrawImage(Images.transparentRed, GameModell.Map[y, x].PositionOnFormPoint);
                }

            foreach (var item in GameModell.Resourse)
                if (item != null)
                    e.Graphics.DrawImage(Stick.GetImage, item.PositionOnFormPoint);

            if (GameModell.Raft != null)
                e.Graphics.DrawImage(GameModell.Raft.GetImage(), GameModell.Raft.PositionOnFormPoint);

            e.Graphics.DrawImage(GameModell.player.GetImage(), GameModell.player.PositionOnFormPoint);

            e.Graphics.DrawString(GameModell.player.CountResourse + @"/" + GameModell.ResoutseToRaft, new Font("Arial", 28), Brushes.Gray, 10, 10);

            e.Graphics.DrawString($"{GameModell.player.X}, {GameModell.player.Y}", new Font("Arial", 28), Brushes.Gray, 100, 10);

            e.Graphics.DrawString($"{GameModell.WaterLine}", new Font("Arial", 28), Brushes.Gray, 500, 10);

            if (GameModell.Babuin != null)
                e.Graphics.DrawImage(GameModell.Babuin.Image, GameModell.Babuin.PositionOnFormPoint);
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
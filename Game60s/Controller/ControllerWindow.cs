using Game60s.Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Game60s.Controller
{
    public static class ControllerWindow
    {
        //тут все понятно
        internal static HashSet<Keys> KeysPressed = new HashSet<Keys>();
        public static int couinter = 0;
        public static void SetPressedKey(Keys pressedKey) =>
            KeysPressed.Add(pressedKey);

        public static void RemoveKey(Keys unPressedKey) =>
            KeysPressed.Remove(unPressedKey);

        public static void EterationGameModel(int timerTick)
        {
            switch (GameModell.GameState)
            {
                case GameModell.GameStates.GameStart:
                    WaitKeyProcess(timerTick);
                    break;
                case GameModell.GameStates.GameProcess:
                    GameProcess(timerTick);
                    break;
                case GameModell.GameStates.LevelWin:
                    LevelWin(timerTick);
                    break;
                case GameModell.GameStates.LevelLose:
                    LevelLose(timerTick);
                    break;
            }
        }

        private static void LevelLose(int timerTick)
        {
            if (KeysPressed.Count > 0 && timeToWait <= 0)
            {
                GameModell.GameLevel = 0;
                GameModell.RestartGameModell();
                GameModell.GameState = GameModell.GameStates.GameProcess;
                timeToWait = 100;
            }
            timeToWait--;
        }

        static int timeToWait = 100;
        private static void LevelWin(int timerTick)
        {
            if (KeysPressed.Count > 0 && timeToWait <= 0)
            {
                //GameModell.SetLevelDifficulty(GameModell.GameLevel, 1000 - GameModell.GameLevel * 150);
                GameModell.NextGameLevel();
                GameModell.GameState = GameModell.GameStates.GameProcess;
                timeToWait = 100;
            }
            timeToWait--;
        }
        private static void WaitKeyProcess(int timerTick)
        {
            if (KeysPressed.Count > 0)
                GameModell.GameState = GameModell.GameStates.GameProcess;
        }

        public static void GameProcess(int timerTick)
        {
            GameModell.player?.Act(KeysPressed);
            GameModell.Raft?.Act(KeysPressed);
            GameModell.Babuin?.Act(KeysPressed);
            GameModell.Babuin?.TryAttack();


            GameModell.player?.TryGetThis(GameModell.Resourse);
            GameModell.Babuin?.TryGetThis(GameModell.Resourse);

            if (timerTick % 10 == 0)
                GameModell.Map.SwitchBorder();

            if (timerTick % GameModell.TickToWaterLineUp == 0)
            {
                Logger.CreateNewLog(GameModell.Map, GameModell.Resourse, GameModell.WaterLine);
                GameModell.IncreaseWaterLine();
                foreach (var item in GameModell.Resourse)
                    if (item != null && item.HitBox.IsOnOcean())
                        item.Dispose();
            }
            if (GameModell.Raft == null && GameModell.ResoutseToRaft <= GameModell.player?.CountResourse)
                GameModell.Raft = new Raft(GameModell.player);

            if (GameModell.Raft == null && GameModell.ResoutseToRaft <= GameModell.Babuin?.CountResourse)
                GameModell.Raft = new Raft(GameModell.Babuin);

            if (GameModell.player?.CountResourse == GameModell.ResoutseToRaft)
                GameModell.GameState = GameModell.GameStates.LevelWin;

            // можно сделать разные концовки игры, типо умер из-за того, что затопило, макака собрала плот быстрее, и тд
            if (GameModell.player != null && GameModell.player.HitBox.IsOnOcean())
                couinter++;

            if (GameModell.ResoutseToRaft <= GameModell.Babuin?.CountResourse)
                GameModell.GameState = GameModell.GameStates.LevelLose;
        }
    }
}

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
                GameModell.ReloadGameModell();
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
                GameModell.ReloadGameModell();
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

        private static void GameProcess(int timerTick)
        {
            GameModell.player.Act(KeysPressed);
            GameModell.Raft?.Act(KeysPressed);
            GameModell.Babuin.Act(KeysPressed);
            GameModell.Babuin.TryAttack();

            foreach (var item in GameModell.ResoursesOnMap)
                item.DieIfOnOcean();


            GameModell.player.TryGetThis(GameModell.Resourse);
            GameModell.Babuin.TryGetThis(GameModell.Resourse);

            if (timerTick % 10 == 0)
                GameModell.Map.SwitchBorder();

            if (timerTick % GameModell.TickToWaterLineUp == 0)
                GameModell.IncreaseWaterLine();

            if (GameModell.Raft == null && GameModell.ResoutseToRaft <= GameModell.player.CountResourse)
                GameModell.Raft = new Raft(GameModell.player);

            if (GameModell.Raft == null && GameModell.ResoutseToRaft <= GameModell.Babuin.CountResourse)
                GameModell.Raft = new Raft(GameModell.Babuin);

            if (GameModell.player.CountResourse == GameModell.ResoutseToRaft)
                GameModell.GameState = GameModell.GameStates.LevelWin;

            if (GameModell.ResoutseToRaft <= GameModell.Babuin.CountResourse)
                GameModell.GameState = GameModell.GameStates.LevelLose;
        }
    }
}

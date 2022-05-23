using Game60s.Model;
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
                case GameModell.GameStates.LevelStart:
                    WaitKeyProcess(timerTick);
                    break;
                case GameModell.GameStates.GameOver:
                    WaitKeyProcess(timerTick);
                    break;
                default:
                    GameProcess(timerTick);
                    break;
            }
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

            if (GameModell.Raft == null && GameModell.ResoutseToRaft <= GameModell.player.CountResourse && KeysPressed.Contains(Keys.R))
                GameModell.Raft = new Raft(GameModell.player);

            if (GameModell.Raft == null && GameModell.ResoutseToRaft <= GameModell.Babuin.CountResourse)
                GameModell.Raft = new Raft(GameModell.Babuin);
        }
    }
}

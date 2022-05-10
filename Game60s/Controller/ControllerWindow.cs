﻿using Game60s.Model;
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
            GameModell.player.Act(KeysPressed);
            GameModell.Raft?.Act(KeysPressed);

            foreach (var item in GameModell.Resourse)
                if (item != null)
                    item.TryGetThis(GameModell.player);
            
            if (timerTick % 10 == 0)
                GameModell.Map.SwitchBorder();

            if (timerTick % GameModell.TickToWaterLineUp == 0) 
                GameModell.IncreaseWaterLine();   

            if (GameModell.Raft == null && GameModell.ResoutseToRaft <= GameModell.player.CountResourse && KeysPressed.Contains(Keys.R))
                GameModell.Raft = new Raft(GameModell.player);
        }
    }
}

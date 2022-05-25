using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Game60s.Model
{
    internal class Player : AEntity, ICanCollect, ICanUseRaft
    {
        Size sizePng = new Size(28, 46);
        int step = 2;
        public List<Resourse> Resourses = new List<Resourse>();
        public bool OnRaft { get; set; }
        public int CountResourse { get; set; }
        public void IncrementResourse() => CountResourse++;
        public Bitmap Image { get => Images.player; }
        public int StanTime { get; set; }
        public int StanResist { get; set; }
        public Player(int x, int y)
        {
            X = x; Y = y;
        }

        public override void Act(HashSet<Keys> keys)
        {
            if (StanTime > 0)
            {
                StanTime -= 1;
                return;
            }

            if (StanResist > 0)
                StanResist -= 1;

            //Добавь ограничения на ходьбу
            if (!OnRaft)
            {
                if (keys.Contains(Keys.ShiftKey))
                    step = 3;
                else if (keys.Contains(Keys.ControlKey))
                    step = 1;
                else
                    step = 2;

                if ((keys.Contains(Keys.Left)
                    || keys.Contains(Keys.A)))
                    //&& GameModell.Map[(X - step - GameModell.ElementSize) / GameModell.ElementSize, Y / GameModell.ElementSize] is Ground)
                    X -= step;
                if ((keys.Contains(Keys.Right) 
                    || keys.Contains(Keys.D)))
                    //&& GameModell.Map[(X + step + GameModell.ElementSize) / GameModell.ElementSize, Y / GameModell.ElementSize] is Ground)
                    X += step;
                if ((keys.Contains(Keys.Up) 
                    || keys.Contains(Keys.W)))
                    //&& GameModell.Map[X / GameModell.ElementSize, (Y - step - GameModell.ElementSize) / GameModell.ElementSize] is Ground)
                    Y -= step;
                if ((keys.Contains(Keys.Down) 
                    || keys.Contains(Keys.S)))
                    //&& GameModell.Map[X / GameModell.ElementSize, (Y + step + GameModell.ElementSize) / GameModell.ElementSize] is Ground)
                    Y += step;
            }
            else
                this.ActOnRaft();
        }

        public void TryGetThis(Resourse[] resourses)
        {
            foreach (var p in resourses)
                if (p != null && Math.Abs(p.X - X) < 20 && Math.Abs(p.Y - Y) < 40)
                    GetThis(p);
        }

        public void GetThis(Resourse res)
        {
            this.IncrementResourse();
            res.Dispose();
        }
        public override AEntity Die() => this;
    }
}

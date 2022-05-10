using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Numerics;

namespace Game60s.Model
{
    /// <summary>
    /// сам по себе он просто собирает ресурсы, если игрок подходит
    /// на определенную дистанцию, то бабуин агрится и с какой-то вероятностью выйгрывает
    /// выйгрыш: забрал случаный ресурс, проигрыш: отдал случаный ресурс.
    /// если игрок проиграл, то он оглушается на 1с, а бабуин отходит.
    /// Если у игрока появляется оружее, то шансы выйграт возрастают.
    /// </summary>
    internal class Babuin : AEntity, ICanCollect, ICanUseRaft
    {
        public int CountResourse { get; set; }
        public bool OnRaft { get; set; }

        public void IncrementResourse() => CountResourse++;
        private const int step = 1;
        public Babuin(int x, int y)
        {
            X = x; Y = y;
        }                                       

        public override void Act(HashSet<Keys> key)
        {
           var t = PositionOnFormV2.MinVector2(GameModell.Resourse.Where(x => x != null).Select(x => x.PositionOnFormV2));
            if (t.X != 0)
                X += t.X < 0 ? -step : step;
            if (t.Y != 0)
                Y += t.Y < 0 ? -step : step;

            if (OnRaft)
            {
                GameModell.Raft.X = X - 50;
                GameModell.Raft.Y = Y - 100;
            }
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

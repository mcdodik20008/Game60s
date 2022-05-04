using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Game60s.Model.Entity
{
    /// <summary>
    /// сам по себе он просто собирает ресурсы, если игрок подходит
    /// на определенную дистанцию, то бабуин агрится и с какой-то вероятностью выйгрывает
    /// выйгрыш: забрал случаный ресурс, проигрыш: отдал случаный ресурс.
    /// если игрок проиграл, то он оглушается на 1с, а бабуин отходит.
    /// Если у игрока появляется оружее, то шансы выйграт возрастают.
    /// </summary>
    internal class Babuin : AEntity
    {
        public Babuin(int x, int y)
        {
            X = x; Y = y;
            PositionOnForm = new Point(x, y);
        }                                       

        public override void Act(HashSet<Keys> key)
        {
            //искуственнй интелект передвижения по сбору ресурсов
            throw new NotImplementedException();
        }

        public override AEntity Die() => this;
    }
}

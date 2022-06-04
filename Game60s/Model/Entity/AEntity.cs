using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;

namespace Game60s.Model
{
    internal abstract class AEntity
    {
        public int X, Y;
        public HitBox HitBox;
        public Vector2 PositionOnFormV2 { get { return new Vector2(X, Y); } }
        public Point PositionOnFormPoint { get { return new Point(X, Y); } }
        public abstract void Act(HashSet<Keys> key);
        public abstract AEntity Die();
        public void ActOnRaft()
        {
            X = GameModell.Raft.X + 50;
            Y = GameModell.Raft.Y + 100;
        }
        public override string ToString() =>
            $"({GetType().Name.Split('.').Last().First()}, {HitBox.LeftUp.X}, {HitBox.LeftUp.Y})";
    }

    internal static class AEntityExtention
    {
        public static Bitmap GetImage(this AEntity entity)
        {
            if (entity is Ground ground)
                if (ground.GroundType == GroundType.grass)
                    return GameModell.EntityImage[$"{ground.GroundType}.png"];
                else
                    return GameModell.EntityImage[$"{ground.GroundType}_{ground.Direction.ConvertDirectionToAngle()}.png"];
            else
            {
                var entityType = entity.GetType().ToString().ToLower().Split('.').Last();
                return GameModell.EntityImage[$"{entityType}.png"];
            }
        }

        public static Vector2 MinVector2(this Vector2 currentPos, IEnumerable<Vector2> nextPosition)
        {
            if (nextPosition.Count() == 0)
                return Vector2.Zero;
            var minV = nextPosition.First() - currentPos;
            foreach (var item in nextPosition)
                if ((item - currentPos).Length() < minV.Length())
                    minV = item - currentPos;
            return minV;
        }

        public static AEntity SetRanomCoordinate(this AEntity ent)
        {
            ent.X = GameModell.Rnd.Next(100, 500);
            ent.Y = GameModell.Rnd.Next(100, 500);
            return ent;
        }


    }
}

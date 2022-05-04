using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Game60s.Model
{
    internal abstract class AEntity
    {
        public int X, Y;
        public int Hp = 3;
        public Point PositionOnForm { get; set; }
        public abstract void Act(HashSet<Keys> key);
        public abstract AEntity Die();
    }

    internal static class AEntityExtention
    {
        public static Bitmap GetImage(this AEntity entity)
        {
            if (entity is Ground ground)
                if(ground.GroundType == GroundType.grass)
                    return GameModell.EntityImage[$"{ground.GroundType}.png"];
                else
                    return GameModell.EntityImage[$"{ground.GroundType}_{ground.Direction.ConvertDirectionToAngle()}.png"];
            else
            {
                var entityType = entity.GetType().ToString().ToLower().Split('.');
                return GameModell.EntityImage[$"{entityType[entityType.Length - 1]}.png"];
            }
        }
    }
}

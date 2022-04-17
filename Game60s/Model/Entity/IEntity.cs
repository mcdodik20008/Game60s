using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

namespace Game60s.Model
{
    internal interface IEntity
    {
        Point PositionOnMap();
        string GetNameImage();
        Vector2 Act(Keys key);
    }
}

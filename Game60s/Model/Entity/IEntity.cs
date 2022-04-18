using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

namespace Game60s.Model
{
    internal interface IEntity
    {
        Point PositionOnMap();
        string GetNameImage();
        void Act(HashSet<Keys> key);
    }
}

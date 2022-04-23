using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game60s.Model
{
    public interface IBorderElement
    {
        DirectionType GetDirection();
        void SetDirection(DirectionType direction);
        bool NeedTurn();
        void SetBorderType(BorderType bT);
    }
}

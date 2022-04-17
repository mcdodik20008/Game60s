using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Game60s.Model;

namespace Game60s.Controller
{
    internal class Controller
    {
        public void GetPressedCey(Keys pressedKey) =>
            GameModell.KeyPressed = pressedKey;
        
    }
}

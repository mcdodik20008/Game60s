using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game60s
{
    public static class Program
    {
        public static void Main()
        {
            var clientSize = new Size(800, 850);
            new GameModell(clientSize);
            Application.Run(new MainWindow()
            {
                ClientSize = clientSize,
                StartPosition = FormStartPosition.CenterScreen,
                MaximumSize = clientSize,
                MinimumSize = clientSize
            });
        }
    }
}

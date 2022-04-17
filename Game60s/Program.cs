using Game60s.Model;
using Game60s.Viev;
using System.Drawing;
using System.Windows.Forms;

namespace Game60s
{
    public static class Program
    {
        public static void Main()
        {
            var clientSize = new Size(800, 850);
            new GameModell();
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

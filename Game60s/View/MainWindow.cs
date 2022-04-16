using System.Drawing;
using System.Windows.Forms;

namespace Viev
{
    internal class MainWindow : Form
    {
        public Size ClientSize { get; set; }
        public Size MaximumSize { get; set; }
        public Size MinimumSize { get; set; }
        public MainWindow()
        {
        }
    }
}
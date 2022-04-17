using System.Windows.Forms;

namespace Game60s.Model
{
    public class GameModell
    {
        public const int ElementSize = 65;
        public const int SizeVisibleMap = 12;
        internal static Map Map;
        internal static Keys KeyPressed;

        internal GameModell()
        {
            Map = MapCreator.Create();
        }

        public void Act()
        {
            foreach (IEntity[] row in Map)
                foreach (var item in row)
                    item.Act(KeyPressed); //Доделаю
        }
    }
}
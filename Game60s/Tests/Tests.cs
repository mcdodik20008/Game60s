using Game60s.Model;
using Game60s.Controller;
using NUnit.Framework;
using System.Collections.Generic;
using System.Windows.Forms;
namespace Game60s.Tests
{
    public class Tests
    {
        protected static void SetGameModell(string[] strMap)
        {
            new GameModell(new Map(
                MapCreator.GetMapIEntity(
                    MapCreator.GetMapChar(
                        strMap
                    )
                )
             ));
        }
    }
}

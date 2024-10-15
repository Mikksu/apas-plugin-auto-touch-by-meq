using APAS.Plugin.MultiAxisSimualMove;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows;

namespace APAS.Plugin.MultiAxisSimualMoveTests
{
    [TestClass()]
    public class PluginDemoTests
    {
        [TestMethod()]
        public void PluginDemoTest()
        {
            var plugin = new PluginDemo(null, "");
            var win = new Window
            {
                Content = plugin.UserView,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Width = 800,
                Height = 600
            };
            win.ShowDialog();
        }
    }
}
using System.Windows;
using System.Windows.Controls;

namespace Toolkit.UI.Views
{
    public partial class DashboardView : UserControl
    {
        public DashboardView()
        {
            InitializeComponent();
        }

        private void OpenTool_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button?.Tag is string tag)
            {
                var mainWindow = Window.GetWindow(this) as MainWindow;
                mainWindow?.OpenTab(tag);
            }
        }
    }
}
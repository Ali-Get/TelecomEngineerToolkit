using System.Windows;
using System.Windows.Controls;

namespace Toolkit.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void OpenTab(string tag)
        {
            foreach (TabItem tab in MainTabControl.Items)
            {
                if (tab.Tag != null && tab.Tag.ToString() == tag)
                {
                    MainTabControl.SelectedItem = tab;
                    break;
                }
            }
        }
    }
}
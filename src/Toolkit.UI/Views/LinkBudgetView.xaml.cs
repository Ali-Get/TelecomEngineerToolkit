using System.Windows;
using System.Windows.Controls;
using Toolkit.Application.ViewModels;

namespace Toolkit.UI.Views
{
    public partial class LinkBudgetView : UserControl
    {
        public LinkBudgetView()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var vm = (LinkBudgetViewModel)DataContext;
            var saveWindow = new SaveCalculationWindow();
            if (saveWindow.ShowDialog() == true)
            {
                vm.SaveCalculation(saveWindow.ProjectName, saveWindow.Notes);
            }
        }
    }
}
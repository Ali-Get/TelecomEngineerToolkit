using System.Windows;
using System.Windows.Controls;
using Toolkit.Application.ViewModels;

namespace Toolkit.UI.Views
{
    public partial class FiberBudgetView : UserControl
    {
        public FiberBudgetView()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var vm = (FiberBudgetViewModel)DataContext;
            var saveWindow = new SaveCalculationWindow();
            if (saveWindow.ShowDialog() == true)
            {
                vm.SaveCalculation(saveWindow.ProjectName, saveWindow.Notes);
            }
        }
    }
}
using System.Windows;
using System.Windows.Controls;
using System.Text;
using Microsoft.Win32;
using Toolkit.Application.ViewModels;
using System.IO;

namespace Toolkit.UI.Views
{
    public partial class ProjectsView : UserControl
    {
        public ProjectsView()
        {
            InitializeComponent();
        }

        private void ExportCsv_Click(object sender, RoutedEventArgs e)
        {
            var vm = (ProjectsViewModel)DataContext;
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                FileName = "SavedCalculations.csv"
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                var path = saveFileDialog.FileName;
                var sb = new StringBuilder();
                sb.AppendLine("Project,Tool,Date,Notes");
                foreach (var calc in vm.SavedCalculations)
                {
                    sb.AppendLine($"{calc.ProjectName},{calc.ToolName},{calc.CreatedAt:yyyy-MM-dd HH:mm},{calc.Notes}");
                }
                File.WriteAllText(path, sb.ToString());
                MessageBox.Show("Exported successfully!", "Export", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
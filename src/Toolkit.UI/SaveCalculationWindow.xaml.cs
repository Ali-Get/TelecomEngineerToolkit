using System.Windows;

namespace Toolkit.UI
{
    public partial class SaveCalculationWindow : Window
    {
        public string ProjectName => ProjectNameBox.Text.Trim();
        public string Notes => NotesBox.Text.Trim();

        public SaveCalculationWindow()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ProjectName))
            {
                MessageBox.Show("Please enter a project name.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
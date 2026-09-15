using System.Collections.ObjectModel;
using System.Windows.Input;
using Toolkit.Application.ViewModels.Base;
using Toolkit.Infrastructure.Models;
using Toolkit.Infrastructure.Services;

namespace Toolkit.Application.ViewModels
{
    public class ProjectsViewModel : BaseViewModel
    {
        private ObservableCollection<SavedCalculation> _savedCalculations = new();
        public ObservableCollection<SavedCalculation> SavedCalculations
        {
            get => _savedCalculations;
            set => SetProperty(ref _savedCalculations, value);
        }

        private SavedCalculation? _selectedCalculation;
        public SavedCalculation? SelectedCalculation
        {
            get => _selectedCalculation;
            set => SetProperty(ref _selectedCalculation, value);
        }

        public ICommand RefreshCommand { get; }
        public ICommand DeleteCommand { get; }

        public ProjectsViewModel()
        {
            RefreshCommand = new RelayCommand(LoadCalculations);
            DeleteCommand = new RelayCommand(DeleteSelectedCalculation);
            LoadCalculations();
        }

        private void LoadCalculations()
        {
            SavedCalculations = new ObservableCollection<SavedCalculation>(DatabaseService.GetAllSavedCalculations());
        }

        private void DeleteSelectedCalculation()
        {
            if (SelectedCalculation == null) return;
            DatabaseService.DeleteSavedCalculation(SelectedCalculation.Id);
            LoadCalculations();
        }
    }
}
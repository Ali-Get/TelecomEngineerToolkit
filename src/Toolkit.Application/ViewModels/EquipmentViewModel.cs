using System.Collections.ObjectModel;
using System.Windows.Input;
using Toolkit.Application.ViewModels.Base;
using Toolkit.Infrastructure.Models;
using Toolkit.Infrastructure.Services;

namespace Toolkit.Application.ViewModels
{
    public class EquipmentViewModel : BaseViewModel
    {
        private ObservableCollection<EquipmentItem> _equipment = new();
        public ObservableCollection<EquipmentItem> Equipment
        {
            get => _equipment;
            set => SetProperty(ref _equipment, value);
        }

        private EquipmentItem? _selectedEquipment;
        public EquipmentItem? SelectedEquipment
        {
            get => _selectedEquipment;
            set
            {
                if (SetProperty(ref _selectedEquipment, value) && value != null)
                {
                    Category = value.Category;
                    Manufacturer = value.Manufacturer;
                    Model = value.Model;
                    Type = value.Type;
                    FrequencyMHz = value.FrequencyMHz?.ToString() ?? "";
                    PowerWatts = value.PowerWatts?.ToString() ?? "";
                    Voltage = value.Voltage?.ToString() ?? "";
                    Gain = value.Gain?.ToString() ?? "";
                    Specifications = value.Specifications;
                    Notes = value.Notes;
                }
            }
        }

        private string _category = "";
        public string Category { get => _category; set => SetProperty(ref _category, value); }

        private string _manufacturer = "";
        public string Manufacturer { get => _manufacturer; set => SetProperty(ref _manufacturer, value); }

        private string _model = "";
        public string Model { get => _model; set => SetProperty(ref _model, value); }

        private string _type = "";
        public string Type { get => _type; set => SetProperty(ref _type, value); }

        private string _frequencyMHz = "";
        public string FrequencyMHz { get => _frequencyMHz; set => SetProperty(ref _frequencyMHz, value); }

        private string _powerWatts = "";
        public string PowerWatts { get => _powerWatts; set => SetProperty(ref _powerWatts, value); }

        private string _voltage = "";
        public string Voltage { get => _voltage; set => SetProperty(ref _voltage, value); }

        private string _gain = "";
        public string Gain { get => _gain; set => SetProperty(ref _gain, value); }

        private string _specifications = "";
        public string Specifications { get => _specifications; set => SetProperty(ref _specifications, value); }

        private string _notes = "";
        public string Notes { get => _notes; set => SetProperty(ref _notes, value); }

        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ClearCommand { get; }

        public EquipmentViewModel()
        {
            AddCommand = new RelayCommand(AddEquipment);
            UpdateCommand = new RelayCommand(UpdateEquipment);
            DeleteCommand = new RelayCommand(DeleteEquipment);
            ClearCommand = new RelayCommand(ClearForm);

            LoadEquipment();
        }

        private void LoadEquipment()
        {
            Equipment = new ObservableCollection<EquipmentItem>(DatabaseService.GetAllEquipment());
        }

        private void AddEquipment()
        {
            var item = new EquipmentItem
            {
                Category = Category,
                Manufacturer = Manufacturer,
                Model = Model,
                Type = Type,
                FrequencyMHz = ParseDouble(FrequencyMHz),
                PowerWatts = ParseDouble(PowerWatts),
                Voltage = ParseDouble(Voltage),
                Gain = ParseDouble(Gain),
                Specifications = Specifications,
                Notes = Notes
            };
            DatabaseService.AddEquipment(item);
            LoadEquipment();
            ClearForm();
        }

        private void UpdateEquipment()
        {
            if (SelectedEquipment == null) return;
            var item = SelectedEquipment;
            item.Category = Category;
            item.Manufacturer = Manufacturer;
            item.Model = Model;
            item.Type = Type;
            item.FrequencyMHz = ParseDouble(FrequencyMHz);
            item.PowerWatts = ParseDouble(PowerWatts);
            item.Voltage = ParseDouble(Voltage);
            item.Gain = ParseDouble(Gain);
            item.Specifications = Specifications;
            item.Notes = Notes;

            DatabaseService.UpdateEquipment(item);
            LoadEquipment();
            ClearForm();
        }

        private void DeleteEquipment()
        {
            if (SelectedEquipment == null) return;
            DatabaseService.DeleteEquipment(SelectedEquipment.Id);
            LoadEquipment();
            ClearForm();
        }

        private void ClearForm()
        {
            Category = "";
            Manufacturer = "";
            Model = "";
            Type = "";
            FrequencyMHz = "";
            PowerWatts = "";
            Voltage = "";
            Gain = "";
            Specifications = "";
            Notes = "";
            SelectedEquipment = null;
        }

        private double? ParseDouble(string text)
        {
            if (double.TryParse(text, out double result))
                return result;
            return null;
        }
    }
}
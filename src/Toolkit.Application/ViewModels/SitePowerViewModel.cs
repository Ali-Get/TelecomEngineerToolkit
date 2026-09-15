using System.Collections.ObjectModel;
using System.Windows.Input;
using Engineering.Core.Calculators;
using Toolkit.Application.ViewModels.Base;

namespace Toolkit.Application.ViewModels
{
    public class SitePowerViewModel : BaseViewModel
    {
        public ObservableCollection<SitePowerDevice> Devices { get; } = new();

        private SitePowerCalculator.SitePowerResult? _result;
        public SitePowerCalculator.SitePowerResult? Result
        {
            get => _result;
            set => SetProperty(ref _result, value);
        }

        private string _newDeviceName = "";
        public string NewDeviceName { get => _newDeviceName; set => SetProperty(ref _newDeviceName, value); }

        private int _newQuantity = 1;
        public int NewQuantity { get => _newQuantity; set => SetProperty(ref _newQuantity, value); }

        private double _newPower = 0;
        public double NewPower { get => _newPower; set => SetProperty(ref _newPower, value); }

        private double _newHours = 24;
        public double NewHours { get => _newHours; set => SetProperty(ref _newHours, value); }

        public ICommand AddDeviceCommand { get; }
        public ICommand CalculateCommand { get; }
        public ICommand ClearCommand { get; }

        public SitePowerViewModel()
        {
            AddDeviceCommand = new RelayCommand(AddDevice);
            CalculateCommand = new RelayCommand(Calculate);
            ClearCommand = new RelayCommand(ClearAll);

            Devices.Add(new SitePowerDevice { Name = "BTS 1", Quantity = 1, PowerWatts = 200, OperatingHoursPerDay = 24 });
            Devices.Add(new SitePowerDevice { Name = "Microwave", Quantity = 1, PowerWatts = 80, OperatingHoursPerDay = 24 });
            Calculate();
        }

        private void AddDevice()
        {
            if (string.IsNullOrWhiteSpace(NewDeviceName) || NewPower <= 0 || NewQuantity <= 0) return;
            Devices.Add(new SitePowerDevice
            {
                Name = NewDeviceName,
                Quantity = NewQuantity,
                PowerWatts = NewPower,
                OperatingHoursPerDay = NewHours
            });
            NewDeviceName = "";
            NewQuantity = 1;
            NewPower = 0;
            NewHours = 24;
            Calculate();
        }

        private void Calculate()
        {
            Result = SitePowerCalculator.Calculate(Devices);
        }

        private void ClearAll()
        {
            Devices.Clear();
            Calculate();
        }
    }
}
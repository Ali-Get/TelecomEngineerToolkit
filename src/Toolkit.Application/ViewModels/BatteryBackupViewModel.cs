using System;
using System.Windows.Input;
using Engineering.Core.Calculators;
using Toolkit.Application.ViewModels.Base;

namespace Toolkit.Application.ViewModels
{
    public class BatteryBackupViewModel : BaseViewModel
    {
        private double _loadW = 500;
        public double LoadW { get => _loadW; set => SetProperty(ref _loadW, value); }

        private double _voltage = 48;
        public double Voltage { get => _voltage; set => SetProperty(ref _voltage, value); }

        private double _batteryAh = 100;
        public double BatteryAh { get => _batteryAh; set => SetProperty(ref _batteryAh, value); }

        private int _numBatteries = 4;
        public int NumBatteries { get => _numBatteries; set => SetProperty(ref _numBatteries, value); }

        private double _efficiency = 90;
        public double Efficiency { get => _efficiency; set => SetProperty(ref _efficiency, value); }

        private double _dod = 50;
        public double Dod { get => _dod; set => SetProperty(ref _dod, value); }

        private double _requiredBackupHours = 8;
        public double RequiredBackupHours { get => _requiredBackupHours; set => SetProperty(ref _requiredBackupHours, value); }

        private BatteryBackupCalculator.BatteryResult? _result;
        public BatteryBackupCalculator.BatteryResult? Result
        {
            get => _result;
            set => SetProperty(ref _result, value);
        }

        public ICommand CalculateCommand { get; }
        public ICommand ClearCommand { get; }

        public BatteryBackupViewModel()
        {
            CalculateCommand = new RelayCommand(Calculate);
            ClearCommand = new RelayCommand(Reset);
            Calculate();
        }

        private void Calculate()
        {
            try
            {
                Result = BatteryBackupCalculator.Calculate(
                    LoadW, Voltage, BatteryAh, NumBatteries,
                    Efficiency, Dod, RequiredBackupHours);
            }
            catch { Result = null; }
        }

        private void Reset()
        {
            LoadW = 500;
            Voltage = 48;
            BatteryAh = 100;
            NumBatteries = 4;
            Efficiency = 90;
            Dod = 50;
            RequiredBackupHours = 8;
            Calculate();
        }
    }
}
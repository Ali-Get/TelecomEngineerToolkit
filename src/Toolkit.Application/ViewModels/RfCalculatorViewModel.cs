using System;
using System.Windows.Input;
using Engineering.Core.Calculators;
using Engineering.Core.Helpers;
using Toolkit.Application.ViewModels.Base;

namespace Toolkit.Application.ViewModels
{
    public class RfCalculatorViewModel : BaseViewModel
    {
        private double _frequencyMHz = 100.0;
        public double FrequencyMHz
        {
            get => _frequencyMHz;
            set
            {
                if (value > 0 && SetProperty(ref _frequencyMHz, value))
                {
                    CalculateWavelength();
                    OnPropertyChanged(nameof(FrequencyDisplay));
                }
            }
        }

        private double _wavelengthM;
        public double WavelengthM
        {
            get => _wavelengthM;
            set
            {
                if (value > 0 && SetProperty(ref _wavelengthM, value))
                {
                    CalculateFrequency();
                    OnPropertyChanged(nameof(FrequencyDisplay));
                }
            }
        }

        private string _calculationEquation = "";
        public string CalculationEquation { get => _calculationEquation; set => SetProperty(ref _calculationEquation, value); }

        private string _errorMessage = "";
        public string ErrorMessage { get => _errorMessage; set => SetProperty(ref _errorMessage, value); }

        private double _dbmValue = 0;
        public double DbmValue
        {
            get => _dbmValue;
            set { if (SetProperty(ref _dbmValue, value)) ConvertDbmToWatt(); }
        }

        private double _wattValue;
        public double WattValue
        {
            get => _wattValue;
            set { if (value > 0 && SetProperty(ref _wattValue, value)) ConvertWattToDbm(); }
        }

        public string FrequencyDisplay => $"Frequency: {FrequencyMHz} MHz";

        public ICommand ClearCommand { get; }

        public RfCalculatorViewModel()
        {
            ClearCommand = new RelayCommand(Reset);
            CalculateWavelength();
            DbmValue = 0;
            ConvertDbmToWatt();
        }

        private void CalculateWavelength()
        {
            try
            {
                WavelengthM = RfCalculator.FrequencyToWavelength(FrequencyMHz);
                CalculationEquation = $"λ = c / f = {RfCalculator.SpeedOfLight} / ({FrequencyMHz}×10^6) = {WavelengthM:E3} m";
                ErrorMessage = "";
            }
            catch (Exception ex) { ErrorMessage = ex.Message; }
        }

        private void CalculateFrequency()
        {
            try
            {
                FrequencyMHz = RfCalculator.WavelengthToFrequency(WavelengthM);
                CalculationEquation = $"f = c / λ = {RfCalculator.SpeedOfLight} / {WavelengthM} = {FrequencyMHz} MHz";
                ErrorMessage = "";
            }
            catch (Exception ex) { ErrorMessage = ex.Message; }
        }

        private void ConvertDbmToWatt()
        {
            try
            {
                WattValue = UnitConverter.DbmToWatt(DbmValue);
                CalculationEquation += $"\nPower: {DbmValue} dBm = {WattValue:E3} W";
            }
            catch (Exception ex) { ErrorMessage = ex.Message; }
        }

        private void ConvertWattToDbm()
        {
            try
            {
                DbmValue = UnitConverter.WattToDbm(WattValue);
                CalculationEquation += $"\nPower: {WattValue} W = {DbmValue:F2} dBm";
            }
            catch (Exception ex) { ErrorMessage = ex.Message; }
        }

        private void Reset()
        {
            FrequencyMHz = 100;
            WavelengthM = 2.99792;
            DbmValue = 0;
            WattValue = 0.001;
            CalculationEquation = "";
            ErrorMessage = "";
        }
    }
}
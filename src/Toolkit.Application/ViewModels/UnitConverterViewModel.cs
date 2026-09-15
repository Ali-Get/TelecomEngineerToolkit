using System.Collections.Generic;
using System.Windows.Input;
using Engineering.Core.Helpers;
using Toolkit.Application.ViewModels.Base;

namespace Toolkit.Application.ViewModels
{
    public class UnitConverterViewModel : BaseViewModel
    {
        private double _inputValue;
        public double InputValue { get => _inputValue; set { SetProperty(ref _inputValue, value); Convert(); } }

        public List<string> ConversionTypes { get; } = new()
        {
            "dBm to Watt", "Watt to dBm",
            "MHz to GHz", "GHz to MHz",
            "m to km", "km to m",
            "°C to °F", "°F to °C"
        };

        private string _conversionType = "dBm to Watt";
        public string SelectedConversionType
        {
            get => _conversionType;
            set { if (SetProperty(ref _conversionType, value)) Convert(); }
        }

        private double _outputValue;
        public double OutputValue { get => _outputValue; set => SetProperty(ref _outputValue, value); }

        private string _outputUnit = "";
        public string OutputUnit { get => _outputUnit; set => SetProperty(ref _outputUnit, value); }

        public ICommand ConvertCommand { get; }

        public UnitConverterViewModel()
        {
            ConvertCommand = new RelayCommand(Convert);
            InputValue = 0;
        }

        private void Convert()
        {
            try
            {
                switch (SelectedConversionType)
                {
                    case "dBm to Watt":
                        OutputValue = UnitConversionService.DbmToWatt(InputValue);
                        OutputUnit = "W";
                        break;
                    case "Watt to dBm":
                        OutputValue = UnitConversionService.WattToDbm(InputValue);
                        OutputUnit = "dBm";
                        break;
                    case "MHz to GHz":
                        OutputValue = InputValue / 1000.0;
                        OutputUnit = "GHz";
                        break;
                    case "GHz to MHz":
                        OutputValue = InputValue * 1000.0;
                        OutputUnit = "MHz";
                        break;
                    case "m to km":
                        OutputValue = InputValue / 1000.0;
                        OutputUnit = "km";
                        break;
                    case "km to m":
                        OutputValue = InputValue * 1000.0;
                        OutputUnit = "m";
                        break;
                    case "°C to °F":
                        OutputValue = UnitConversionService.CelsiusToFahrenheit(InputValue);
                        OutputUnit = "°F";
                        break;
                    case "°F to °C":
                        OutputValue = UnitConversionService.FahrenheitToCelsius(InputValue);
                        OutputUnit = "°C";
                        break;
                }
            }
            catch { }
        }
    }
}
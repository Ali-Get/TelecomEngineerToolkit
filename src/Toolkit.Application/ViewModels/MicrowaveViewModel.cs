using System;
using System.Windows.Input;
using Engineering.Core.Calculators;
using Toolkit.Application.ViewModels.Base;

namespace Toolkit.Application.ViewModels
{
    public class MicrowaveViewModel : BaseViewModel
    {
        private double _latA = 33.3128;
        public double LatA { get => _latA; set => SetProperty(ref _latA, value); }

        private double _lonA = 44.3615;
        public double LonA { get => _lonA; set => SetProperty(ref _lonA, value); }

        private double _heightA = 30;
        public double HeightA { get => _heightA; set => SetProperty(ref _heightA, value); }

        private double _latB = 33.3150;
        public double LatB { get => _latB; set => SetProperty(ref _latB, value); }

        private double _lonB = 44.3700;
        public double LonB { get => _lonB; set => SetProperty(ref _lonB, value); }

        private double _heightB = 30;
        public double HeightB { get => _heightB; set => SetProperty(ref _heightB, value); }

        private double _frequencyMHz = 18000;
        public double FrequencyMHz { get => _frequencyMHz; set => SetProperty(ref _frequencyMHz, value); }

        private double _txPowerDbm = 20;
        public double TxPowerDbm { get => _txPowerDbm; set => SetProperty(ref _txPowerDbm, value); }

        private double _antennaGainDbi = 30;
        public double AntennaGainDbi { get => _antennaGainDbi; set => SetProperty(ref _antennaGainDbi, value); }

        private double _cableLossDb = 1;
        public double CableLossDb { get => _cableLossDb; set => SetProperty(ref _cableLossDb, value); }

        private double _connectorLossDb = 0.5;
        public double ConnectorLossDb { get => _connectorLossDb; set => SetProperty(ref _connectorLossDb, value); }

        private double _rxSensitivityDbm = -70;
        public double RxSensitivityDbm { get => _rxSensitivityDbm; set => SetProperty(ref _rxSensitivityDbm, value); }

        private MicrowaveCalculator.MicrowaveResult? _result;
        public MicrowaveCalculator.MicrowaveResult? Result
        {
            get => _result;
            set => SetProperty(ref _result, value);
        }

        public ICommand CalculateCommand { get; }
        public ICommand ClearCommand { get; }

        public MicrowaveViewModel()
        {
            CalculateCommand = new RelayCommand(Calculate);
            ClearCommand = new RelayCommand(Reset);
            Calculate();
        }

        private void Calculate()
        {
            try
            {
                Result = MicrowaveCalculator.Calculate(
                    LatA, LonA, HeightA,
                    LatB, LonB, HeightB,
                    FrequencyMHz, TxPowerDbm, AntennaGainDbi,
                    CableLossDb, ConnectorLossDb, RxSensitivityDbm);
            }
            catch { Result = null; }
        }

        private void Reset()
        {
            LatA = 33.3128;
            LonA = 44.3615;
            HeightA = 30;
            LatB = 33.3150;
            LonB = 44.3700;
            HeightB = 30;
            FrequencyMHz = 18000;
            TxPowerDbm = 20;
            AntennaGainDbi = 30;
            CableLossDb = 1;
            ConnectorLossDb = 0.5;
            RxSensitivityDbm = -70;
            Calculate();
        }
    }
}
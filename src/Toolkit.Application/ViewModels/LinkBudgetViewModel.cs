using System;
using System.Text.Json;
using System.Windows.Input;
using Engineering.Core.Calculators;
using Toolkit.Application.ViewModels.Base;
using Toolkit.Infrastructure.Models;
using Toolkit.Infrastructure.Services;

namespace Toolkit.Application.ViewModels
{
    public class LinkBudgetViewModel : BaseViewModel
    {
        private double _frequencyMHz = 5000;
        public double FrequencyMHz { get => _frequencyMHz; set => SetProperty(ref _frequencyMHz, value); }

        private double _distanceKm = 5;
        public double DistanceKm { get => _distanceKm; set => SetProperty(ref _distanceKm, value); }

        private double _txPowerDbm = 20;
        public double TxPowerDbm { get => _txPowerDbm; set => SetProperty(ref _txPowerDbm, value); }

        private double _txAntennaGain = 20;
        public double TxAntennaGain { get => _txAntennaGain; set => SetProperty(ref _txAntennaGain, value); }

        private double _rxAntennaGain = 20;
        public double RxAntennaGain { get => _rxAntennaGain; set => SetProperty(ref _rxAntennaGain, value); }

        private double _cableLoss = 2;
        public double CableLoss { get => _cableLoss; set => SetProperty(ref _cableLoss, value); }

        private double _connectorLoss = 0.5;
        public double ConnectorLoss { get => _connectorLoss; set => SetProperty(ref _connectorLoss, value); }

        private double _otherLoss = 1;
        public double OtherLoss { get => _otherLoss; set => SetProperty(ref _otherLoss, value); }

        private double _rxSensitivity = -80;
        public double RxSensitivity { get => _rxSensitivity; set => SetProperty(ref _rxSensitivity, value); }

        private LinkBudgetCalculator.LinkBudgetResult? _result;
        public LinkBudgetCalculator.LinkBudgetResult? Result
        {
            get => _result;
            set => SetProperty(ref _result, value);
        }

        public ICommand CalculateCommand { get; }
        public ICommand ClearCommand { get; }

        public LinkBudgetViewModel()
        {
            CalculateCommand = new RelayCommand(Calculate);
            ClearCommand = new RelayCommand(Reset);
            Calculate();
        }

        private void Calculate()
        {
            try
            {
                Result = LinkBudgetCalculator.Calculate(
                    FrequencyMHz, DistanceKm * 1000, TxPowerDbm,
                    TxAntennaGain, RxAntennaGain, CableLoss,
                    ConnectorLoss, OtherLoss, RxSensitivity);
            }
            catch { Result = null; }
        }

        private void Reset()
        {
            FrequencyMHz = 5000;
            DistanceKm = 5;
            TxPowerDbm = 20;
            TxAntennaGain = 20;
            RxAntennaGain = 20;
            CableLoss = 2;
            ConnectorLoss = 0.5;
            OtherLoss = 1;
            RxSensitivity = -80;
            Calculate();
        }

        public void SaveCalculation(string projectName, string notes)
        {
            if (Result == null) return;

            var input = new
            {
                FrequencyMHz,
                DistanceKm,
                TxPowerDbm,
                TxAntennaGain,
                RxAntennaGain,
                CableLoss,
                ConnectorLoss,
                OtherLoss,
                RxSensitivity
            };

            var result = new
            {
                Result.FsplDb,
                Result.TotalLossDb,
                Result.ReceivedPowerDbm,
                Result.FadeMarginDb,
                Result.LinkStatus
            };

            var saved = new SavedCalculation
            {
                ProjectName = projectName,
                ToolName = "Link Budget",
                InputJson = JsonSerializer.Serialize(input),
                ResultJson = JsonSerializer.Serialize(result),
                Notes = notes
            };
            DatabaseService.SaveCalculation(saved);
        }
    }
}
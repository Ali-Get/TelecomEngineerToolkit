using System;
using System.Text.Json;
using System.Windows.Input;
using Engineering.Core.Calculators;
using Toolkit.Application.ViewModels.Base;
using Toolkit.Infrastructure.Models;
using Toolkit.Infrastructure.Services;

namespace Toolkit.Application.ViewModels
{
    public class FiberBudgetViewModel : BaseViewModel
    {
        private double _fiberLengthKm = 10;
        public double FiberLengthKm { get => _fiberLengthKm; set => SetProperty(ref _fiberLengthKm, value); }

        private double _attenuation = 0.35;
        public double Attenuation { get => _attenuation; set => SetProperty(ref _attenuation, value); }

        private int _splices = 3;
        public int Splices { get => _splices; set => SetProperty(ref _splices, value); }

        private double _spliceLoss = 0.1;
        public double SpliceLoss { get => _spliceLoss; set => SetProperty(ref _spliceLoss, value); }

        private int _connectors = 2;
        public int Connectors { get => _connectors; set => SetProperty(ref _connectors, value); }

        private double _connectorLoss = 0.5;
        public double ConnectorLossValue { get => _connectorLoss; set => SetProperty(ref _connectorLoss, value); }

        private double _splitterLoss = 0;
        public double SplitterLoss { get => _splitterLoss; set => SetProperty(ref _splitterLoss, value); }

        private double _otherLosses = 1;
        public double OtherLosses { get => _otherLosses; set => SetProperty(ref _otherLosses, value); }

        private double _txPower = 3;
        public double TxPower { get => _txPower; set => SetProperty(ref _txPower, value); }

        private double _rxSensitivity = -24;
        public double RxSensitivity { get => _rxSensitivity; set => SetProperty(ref _rxSensitivity, value); }

        private FiberLossCalculator.FiberLinkResult? _result;
        public FiberLossCalculator.FiberLinkResult? Result
        {
            get => _result;
            set => SetProperty(ref _result, value);
        }

        public ICommand CalculateCommand { get; }
        public ICommand ClearCommand { get; }

        public FiberBudgetViewModel()
        {
            CalculateCommand = new RelayCommand(Calculate);
            ClearCommand = new RelayCommand(Reset);
            Calculate();
        }

        private void Calculate()
        {
            try
            {
                Result = FiberLossCalculator.Calculate(
                    FiberLengthKm, Attenuation, Splices, SpliceLoss,
                    Connectors, ConnectorLossValue, SplitterLoss,
                    OtherLosses, TxPower, RxSensitivity);
            }
            catch { Result = null; }
        }

        private void Reset()
        {
            FiberLengthKm = 10;
            Attenuation = 0.35;
            Splices = 3;
            SpliceLoss = 0.1;
            Connectors = 2;
            ConnectorLossValue = 0.5;
            SplitterLoss = 0;
            OtherLosses = 1;
            TxPower = 3;
            RxSensitivity = -24;
            Calculate();
        }

        public void SaveCalculation(string projectName, string notes)
        {
            if (Result == null) return;

            var input = new
            {
                FiberLengthKm,
                Attenuation,
                Splices,
                SpliceLoss,
                Connectors,
                ConnectorLossValue,
                SplitterLoss,
                OtherLosses,
                TxPower,
                RxSensitivity
            };

            var result = new
            {
                Result.TotalFiberLoss,
                Result.TotalConnectorLoss,
                Result.TotalSpliceLoss,
                Result.TotalLinkLoss,
                Result.ExpectedRxPower,
                Result.OpticalMargin,
                Result.Status
            };

            var saved = new SavedCalculation
            {
                ProjectName = projectName,
                ToolName = "Fiber Budget",
                InputJson = JsonSerializer.Serialize(input),
                ResultJson = JsonSerializer.Serialize(result),
                Notes = notes
            };
            DatabaseService.SaveCalculation(saved);
        }
    }
}
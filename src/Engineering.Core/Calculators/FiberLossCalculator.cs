using System;

namespace Engineering.Core.Calculators
{
    public static class FiberLossCalculator
    {
        public class FiberLinkResult
        {
            public double TotalFiberLoss { get; set; }
            public double TotalConnectorLoss { get; set; }
            public double TotalSpliceLoss { get; set; }
            public double SplitterLoss { get; set; }
            public double OtherLosses { get; set; }
            public double TotalLinkLoss { get; set; }
            public double ExpectedRxPower { get; set; }
            public double OpticalMargin { get; set; }
            public string Status { get; set; } = "PASS";
        }

        public static FiberLinkResult Calculate(
            double fiberLengthKm, double attenuationDbPerKm,
            int numberOfSplices, double spliceLossDb,
            int numberOfConnectors, double connectorLossDb,
            double splitterLossDb, double otherLossesDb,
            double txPowerDbm, double rxSensitivityDbm)
        {
            if (fiberLengthKm < 0 || attenuationDbPerKm < 0 || spliceLossDb < 0 ||
                connectorLossDb < 0 || splitterLossDb < 0 || otherLossesDb < 0)
                throw new ArgumentException("All loss values must be >= 0");

            var result = new FiberLinkResult
            {
                TotalFiberLoss = fiberLengthKm * attenuationDbPerKm,
                TotalSpliceLoss = numberOfSplices * spliceLossDb,
                TotalConnectorLoss = numberOfConnectors * connectorLossDb,
                SplitterLoss = splitterLossDb,
                OtherLosses = otherLossesDb
            };
            result.TotalLinkLoss = result.TotalFiberLoss + result.TotalSpliceLoss +
                                   result.TotalConnectorLoss + result.SplitterLoss +
                                   result.OtherLosses;
            result.ExpectedRxPower = txPowerDbm - result.TotalLinkLoss;
            result.OpticalMargin = result.ExpectedRxPower - rxSensitivityDbm;

            result.Status = result.OpticalMargin >= 3 ? "PASS" :
                            result.OpticalMargin >= 0 ? "WARNING" : "FAIL";
            return result;
        }
    }
}
using System;

namespace Engineering.Core.Calculators
{
    public static class LinkBudgetCalculator
    {
        public class LinkBudgetResult
        {
            public double FsplDb { get; set; }
            public double TotalLossDb { get; set; }
            public double ReceivedPowerDbm { get; set; }
            public double FadeMarginDb { get; set; }
            public string LinkStatus { get; set; } = "PASS";
        }

        public static LinkBudgetResult Calculate(
            double frequencyMHz, double distanceM,
            double txPowerDbm, double txAntennaGainDbi,
            double rxAntennaGainDbi, double cableLossDb,
            double connectorLossDb, double otherLossDb,
            double rxSensitivityDbm)
        {
            if (frequencyMHz <= 0 || distanceM <= 0)
                throw new ArgumentException("Frequency and distance must be > 0");

            double fspl = RfCalculator.Fspl(distanceM, frequencyMHz);
            double totalLoss = fspl + cableLossDb + connectorLossDb + otherLossDb;
            double erp = txPowerDbm + txAntennaGainDbi - cableLossDb;
            double rxPower = erp - fspl + rxAntennaGainDbi - connectorLossDb - otherLossDb;
            double fadeMargin = rxPower - rxSensitivityDbm;

            return new LinkBudgetResult
            {
                FsplDb = fspl,
                TotalLossDb = totalLoss,
                ReceivedPowerDbm = rxPower,
                FadeMarginDb = fadeMargin,
                LinkStatus = fadeMargin >= 10 ? "PASS" : (fadeMargin >= 0 ? "MARGINAL" : "FAIL")
            };
        }
    }
}
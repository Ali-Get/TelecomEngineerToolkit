using System;

namespace Engineering.Core.Calculators
{
    public static class MicrowaveCalculator
    {
        public class MicrowaveResult
        {
            public double DistanceKm { get; set; }
            public double LineOfSightApprox { get; set; }
            public double FresnelZoneRadiusM { get; set; }
            public double FsplDb { get; set; }
            public double ReceivedPowerDbm { get; set; }
            public double LinkMarginDb { get; set; }
            public string LinkStatus { get; set; } = "PASS";
        }

        public static MicrowaveResult Calculate(
            double latA, double lonA, double hA,
            double latB, double lonB, double hB,
            double frequencyMHz, double txPowerDbm, double antennaGainDbi,
            double cableLossDb = 0, double connectorLossDb = 0,
            double rxSensitivityDbm = -80)
        {
            const double R = 6371; // km
            double dLat = ToRadians(latB - latA);
            double dLon = ToRadians(lonB - lonA);
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(ToRadians(latA)) * Math.Cos(ToRadians(latB)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distanceKm = R * c;

            double los = 4.12 * (Math.Sqrt(hA) + Math.Sqrt(hB));

            double fGHz = frequencyMHz / 1000.0;
            double fresnelRadius = 17.32 * Math.Sqrt(distanceKm / (4 * fGHz));

            double fspl = RfCalculator.Fspl(distanceKm * 1000, frequencyMHz);

            double rxPower = txPowerDbm + antennaGainDbi - cableLossDb - fspl + antennaGainDbi - connectorLossDb;

            double linkMargin = rxPower - rxSensitivityDbm;
            string status = linkMargin >= 10 ? "PASS" : (linkMargin >= 0 ? "MARGINAL" : "FAIL");

            return new MicrowaveResult
            {
                DistanceKm = distanceKm,
                LineOfSightApprox = los,
                FresnelZoneRadiusM = fresnelRadius,
                FsplDb = fspl,
                ReceivedPowerDbm = rxPower,
                LinkMarginDb = linkMargin,
                LinkStatus = status
            };
        }

        private static double ToRadians(double degrees) => degrees * Math.PI / 180.0;
    }
}
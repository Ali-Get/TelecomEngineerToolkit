using System;

namespace Engineering.Core.Calculators
{
    public static class RfCalculator
    {
        public const double SpeedOfLight = 299792458; // m/s

        public static double FrequencyToWavelength(double frequencyMHz)
        {
            if (frequencyMHz <= 0) throw new ArgumentException("Frequency must be > 0");
            double freqHz = frequencyMHz * 1e6;
            return SpeedOfLight / freqHz;
        }

        public static double WavelengthToFrequency(double wavelengthM)
        {
            if (wavelengthM <= 0) throw new ArgumentException("Wavelength must be > 0");
            return SpeedOfLight / wavelengthM / 1e6;
        }

        public static double Fspl(double distanceM, double frequencyMHz)
        {
            if (distanceM <= 0 || frequencyMHz <= 0)
                throw new ArgumentException("Distance and frequency must be > 0");
            return 20 * Math.Log10(distanceM) + 20 * Math.Log10(frequencyMHz) - 147.55;
        }

        public static double Eirp(double txPowerDbm, double txAntennaGainDbi, double cableLossDb = 0)
        {
            return txPowerDbm + txAntennaGainDbi - cableLossDb;
        }

        public static double ReceivedPower(double eirpDbm, double pathLossDb, double rxGainDbi = 0)
        {
            return eirpDbm - pathLossDb + rxGainDbi;
        }
    }
}
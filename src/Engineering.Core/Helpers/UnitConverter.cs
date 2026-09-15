using System;
using Engineering.Core.Units;
namespace Engineering.Core.Helpers
{
    public static class UnitConverter
    {
        public static double ConvertFrequency(double value, FrequencyUnit from, FrequencyUnit to)
        {
            double toHz = from switch
            {
                FrequencyUnit.Hz => value,
                FrequencyUnit.KHz => value * 1e3,
                FrequencyUnit.MHz => value * 1e6,
                FrequencyUnit.GHz => value * 1e9,
                _ => throw new ArgumentOutOfRangeException()
            };
            return to switch
            {
                FrequencyUnit.Hz => toHz,
                FrequencyUnit.KHz => toHz / 1e3,
                FrequencyUnit.MHz => toHz / 1e6,
                FrequencyUnit.GHz => toHz / 1e9,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public static double WattToDbm(double watt)
        {
            if (watt <= 0) throw new ArgumentException("Power in Watts must be > 0");
            return 10.0 * Math.Log10(watt * 1000.0);
        }

        public static double DbmToWatt(double dbm)
        {
            return Math.Pow(10, (dbm - 30) / 10.0);
        }

        public static double DbmToMilliwatt(double dbm) => Math.Pow(10, dbm / 10.0);
        public static double MilliwattToDbm(double mw) => 10 * Math.Log10(mw);
        public static double WattToMilliwatt(double w) => w * 1000.0;
        public static double MilliwattToWatt(double mw) => mw / 1000.0;

        public static double ConvertDistance(double value, DistanceUnit from, DistanceUnit to)
        {
            double toMeter = from switch
            {
                DistanceUnit.Meter => value,
                DistanceUnit.Kilometer => value * 1000.0,
                DistanceUnit.Mile => value * 1609.34,
                _ => throw new ArgumentOutOfRangeException()
            };
            return to switch
            {
                DistanceUnit.Meter => toMeter,
                DistanceUnit.Kilometer => toMeter / 1000.0,
                DistanceUnit.Mile => toMeter / 1609.34,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
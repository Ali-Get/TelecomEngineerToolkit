using System;

namespace Engineering.Core.Helpers
{
    public static class UnitConversionService
    {
        public static double DbmToWatt(double dbm) => UnitConverter.DbmToWatt(dbm);
        public static double WattToDbm(double watt) => UnitConverter.WattToDbm(watt);
        public static double MwToDbm(double mw) => 10 * Math.Log10(mw);
        public static double DbmToMw(double dbm) => Math.Pow(10, dbm / 10.0);
        public static double FahrenheitToCelsius(double f) => (f - 32) * 5.0 / 9.0;
        public static double CelsiusToFahrenheit(double c) => c * 9.0 / 5.0 + 32;
    }
}
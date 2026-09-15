using System.Collections.Generic;
using System.Linq;

namespace Engineering.Core.Calculators
{
    public class SitePowerDevice
    {
        public string Name { get; set; } = "";
        public int Quantity { get; set; }
        public double PowerWatts { get; set; }
        public double OperatingHoursPerDay { get; set; }
        public double TotalPower => Quantity * PowerWatts;
        public double DailyWh => TotalPower * OperatingHoursPerDay;
    }

    public static class SitePowerCalculator
    {
        public class SitePowerResult
        {
            public double TotalInstantaneousLoad { get; set; }
            public double DailyConsumptionKwh { get; set; }
            public double MonthlyConsumptionKwh { get; set; }
            public double PeakLoad { get; set; }
            public List<SitePowerDevice> Devices { get; set; } = new();
        }

        public static SitePowerResult Calculate(IEnumerable<SitePowerDevice> devices)
        {
            var devList = devices.ToList();
            double totalLoad = devList.Sum(d => d.TotalPower);
            double dailyWh = devList.Sum(d => d.DailyWh);
            return new SitePowerResult
            {
                Devices = devList,
                TotalInstantaneousLoad = totalLoad,
                DailyConsumptionKwh = dailyWh / 1000.0,
                MonthlyConsumptionKwh = (dailyWh * 30) / 1000.0,
                PeakLoad = totalLoad
            };
        }
    }
}
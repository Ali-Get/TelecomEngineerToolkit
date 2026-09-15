using System;

namespace Engineering.Core.Calculators
{
    public static class BatteryBackupCalculator
    {
        public class BatteryResult
        {
            public double TotalCapacityWh { get; set; }
            public double AvailableEnergyWh { get; set; }
            public double EstimatedBackupTimeHours { get; set; }
            public double RequiredCapacityAh { get; set; }
            public int RequiredBatteries { get; set; }
        }

        public static BatteryResult Calculate(
            double loadW, double voltage, double batteryAh,
            int numberOfBatteries, double efficiencyPercent,
            double depthOfDischargePercent, double requiredBackupHours)
        {
            if (loadW <= 0 || voltage <= 0 || batteryAh <= 0 || numberOfBatteries <= 0 ||
                efficiencyPercent <= 0 || efficiencyPercent > 100 ||
                depthOfDischargePercent <= 0 || depthOfDischargePercent > 100)
                throw new ArgumentException("Invalid input values");

            double efficiency = efficiencyPercent / 100.0;
            double dod = depthOfDischargePercent / 100.0;
            double totalCapacityWh = voltage * batteryAh * numberOfBatteries;
            double availableEnergyWh = totalCapacityWh * dod * efficiency;
            double backupTime = availableEnergyWh / loadW;
            double requiredEnergyWh = loadW * requiredBackupHours;
            double requiredCapacityAh = requiredEnergyWh / (voltage * dod * efficiency);
            int requiredBatteries = (int)Math.Ceiling(requiredCapacityAh / batteryAh);

            return new BatteryResult
            {
                TotalCapacityWh = totalCapacityWh,
                AvailableEnergyWh = availableEnergyWh,
                EstimatedBackupTimeHours = backupTime,
                RequiredCapacityAh = requiredCapacityAh,
                RequiredBatteries = requiredBatteries
            };
        }
    }
}
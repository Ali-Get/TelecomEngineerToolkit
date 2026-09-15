using System;

namespace Toolkit.Infrastructure.Models
{
    public class EquipmentItem
    {
        public int Id { get; set; }
        public string Category { get; set; } = "";      // Antenna, RRU, BBU, Router, ...
        public string Manufacturer { get; set; } = "";
        public string Model { get; set; } = "";
        public string Type { get; set; } = "";          // e.g. Sector, Dish, etc.
        public double? FrequencyMHz { get; set; }        // Nullable
        public double? PowerWatts { get; set; }
        public double? Voltage { get; set; }
        public double? Gain { get; set; }
        public string Specifications { get; set; } = "";
        public string Notes { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
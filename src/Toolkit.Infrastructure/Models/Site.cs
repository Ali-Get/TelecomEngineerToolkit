using System;

namespace Toolkit.Infrastructure.Models
{
    public class Site
    {
        public int Id { get; set; }
        public string SiteId { get; set; } = "";       // e.g. "WAS-001"
        public string Name { get; set; } = "";
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double TowerHeight { get; set; }
        public string SiteType { get; set; } = "";      // e.g. Rooftop, Tower, Greenfield
        public string Technology { get; set; } = "";    // e.g. 2G, 3G, 4G, 5G
        public string Notes { get; set; } = "";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
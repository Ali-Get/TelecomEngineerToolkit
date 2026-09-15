using System;
using System.Collections.Generic;

namespace Engineering.Core.Calculators
{
    public class CoverageSite
    {
        public string Name { get; set; } = "";
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double TowerHeight { get; set; }
        public List<Sector> Sectors { get; set; } = new();
    }

    public class Sector
    {
        public string Name { get; set; } = "Sector 1";
        public double Azimuth { get; set; }    // degrees from North
        public double Beamwidth { get; set; }  // degrees
        public double RadiusKm { get; set; }   // approximate coverage radius
    }

    public static class CoveragePlanner
    {
        // Convert lat/lon to simple XY for canvas (equirectangular projection)
        public static (double x, double y) LatLonToXY(double lat, double lon, double centerLat, double centerLon, double scale = 1000)
        {
            double x = (lon - centerLon) * Math.Cos(centerLat * Math.PI / 180.0) * 111.32 * scale;
            double y = -(lat - centerLat) * 110.54 * scale;
            return (x, y);
        }
    }
}
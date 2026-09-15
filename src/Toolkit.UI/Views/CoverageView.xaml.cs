using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Engineering.Core.Calculators;
using Toolkit.Application.ViewModels;

namespace Toolkit.UI.Views
{
    public partial class CoverageView : UserControl
    {
        public CoverageView()
        {
            InitializeComponent();
            DataContextChanged += CoverageView_DataContextChanged;
        }

        private void CoverageView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is CoverageViewModel vm)
            {
                vm.Sites.CollectionChanged += (s, args) => DrawSites();
            }
        }

        private void DrawSites()
        {
            MapCanvas.Children.Clear();
            var vm = (CoverageViewModel)DataContext;

            if (vm.Sites.Count == 0) return;

            // Compute center of all sites
            double avgLat = 0, avgLon = 0;
            foreach (var site in vm.Sites)
            {
                avgLat += site.Latitude;
                avgLon += site.Longitude;
            }
            avgLat /= vm.Sites.Count;
            avgLon /= vm.Sites.Count;

            double scale = 1000; // pixels per degree approx
            double offsetX = MapCanvas.ActualWidth / 2;
            double offsetY = MapCanvas.ActualHeight / 2;

            foreach (var site in vm.Sites)
            {
                var (x, y) = CoveragePlanner.LatLonToXY(site.Latitude, site.Longitude, avgLat, avgLon, scale);
                double centerX = offsetX + x;
                double centerY = offsetY + y;

                // Draw tower as small circle
                Ellipse tower = new Ellipse
                {
                    Width = 10,
                    Height = 10,
                    Fill = Brushes.Black
                };
                Canvas.SetLeft(tower, centerX - 5);
                Canvas.SetTop(tower, centerY - 5);
                MapCanvas.Children.Add(tower);

                // Draw sectors
                foreach (var sector in site.Sectors)
                {
                    double radiusPixels = sector.RadiusKm * 1000 / 10; // arbitrary scaling
                    double startAngle = sector.Azimuth - sector.Beamwidth / 2;
                    double endAngle = sector.Azimuth + sector.Beamwidth / 2;
                    PointCollection points = new PointCollection
                    {
                        new Point(centerX, centerY)
                    };
                    for (double angle = startAngle; angle <= endAngle; angle += 5)
                    {
                        double rad = angle * Math.PI / 180.0;
                        points.Add(new Point(centerX + radiusPixels * Math.Sin(rad), centerY - radiusPixels * Math.Cos(rad)));
                    }
                    points.Add(new Point(centerX, centerY));
                    Polygon sectorShape = new Polygon
                    {
                        Points = points,
                        Fill = new SolidColorBrush(Color.FromArgb(80, 0, 0, 255)),
                        Stroke = Brushes.Blue
                    };
                    MapCanvas.Children.Add(sectorShape);
                }

                // Label
                TextBlock label = new TextBlock
                {
                    Text = site.Name,
                    FontSize = 10
                };
                Canvas.SetLeft(label, centerX + 10);
                Canvas.SetTop(label, centerY - 10);
                MapCanvas.Children.Add(label);
            }
        }
    }
}
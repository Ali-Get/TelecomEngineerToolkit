using System.Collections.ObjectModel;
using System.Windows.Input;
using Engineering.Core.Calculators;
using Toolkit.Application.ViewModels.Base;

namespace Toolkit.Application.ViewModels
{
    public class CoverageViewModel : BaseViewModel
    {
        private ObservableCollection<CoverageSite> _sites = new();
        public ObservableCollection<CoverageSite> Sites
        {
            get => _sites;
            set => SetProperty(ref _sites, value);
        }

        private CoverageSite? _selectedSite;
        public CoverageSite? SelectedSite
        {
            get => _selectedSite;
            set => SetProperty(ref _selectedSite, value);
        }

        // Inputs for new site
        private string _siteName = "";
        public string SiteName { get => _siteName; set => SetProperty(ref _siteName, value); }

        private double _latitude;
        public double Latitude { get => _latitude; set => SetProperty(ref _latitude, value); }

        private double _longitude;
        public double Longitude { get => _longitude; set => SetProperty(ref _longitude, value); }

        private double _towerHeight = 30;
        public double TowerHeight { get => _towerHeight; set => SetProperty(ref _towerHeight, value); }

        // Sector inputs
        private string _sectorName = "Sector 1";
        public string SectorName { get => _sectorName; set => SetProperty(ref _sectorName, value); }

        private double _azimuth;
        public double Azimuth { get => _azimuth; set => SetProperty(ref _azimuth, value); }

        private double _beamwidth = 65;
        public double Beamwidth { get => _beamwidth; set => SetProperty(ref _beamwidth, value); }

        private double _radiusKm = 2;
        public double RadiusKm { get => _radiusKm; set => SetProperty(ref _radiusKm, value); }

        public ICommand AddSiteCommand { get; }
        public ICommand AddSectorCommand { get; }
        public ICommand DeleteSiteCommand { get; }
        public ICommand ClearCommand { get; }

        public CoverageViewModel()
        {
            AddSiteCommand = new RelayCommand(AddSite);
            AddSectorCommand = new RelayCommand(AddSector);
            DeleteSiteCommand = new RelayCommand(DeleteSite);
            ClearCommand = new RelayCommand(ClearAll);
        }

        private void AddSite()
        {
            if (string.IsNullOrWhiteSpace(SiteName)) return;
            var site = new CoverageSite
            {
                Name = SiteName,
                Latitude = Latitude,
                Longitude = Longitude,
                TowerHeight = TowerHeight
            };
            site.Sectors.Add(new Sector
            {
                Name = SectorName,
                Azimuth = Azimuth,
                Beamwidth = Beamwidth,
                RadiusKm = RadiusKm
            });
            Sites.Add(site);
            SiteName = "";
            Latitude = 0;
            Longitude = 0;
            TowerHeight = 30;
        }

        private void AddSector()
        {
            if (SelectedSite == null) return;
            SelectedSite.Sectors.Add(new Sector
            {
                Name = SectorName,
                Azimuth = Azimuth,
                Beamwidth = Beamwidth,
                RadiusKm = RadiusKm
            });
        }

        private void DeleteSite()
        {
            if (SelectedSite != null)
                Sites.Remove(SelectedSite);
        }

        private void ClearAll()
        {
            Sites.Clear();
        }
    }
}
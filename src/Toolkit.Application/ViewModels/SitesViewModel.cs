using System.Collections.ObjectModel;
using System.Windows.Input;
using Toolkit.Application.ViewModels.Base;
using Toolkit.Infrastructure.Models;
using Toolkit.Infrastructure.Services;

namespace Toolkit.Application.ViewModels
{
    public class SitesViewModel : BaseViewModel
    {
        private ObservableCollection<Site> _sites = new();
        public ObservableCollection<Site> Sites
        {
            get => _sites;
            set => SetProperty(ref _sites, value);
        }

        private Site? _selectedSite;
        public Site? SelectedSite
        {
            get => _selectedSite;
            set
            {
                if (SetProperty(ref _selectedSite, value) && value != null)
                {
                    SiteId = value.SiteId;
                    Name = value.Name;
                    Latitude = value.Latitude;
                    Longitude = value.Longitude;
                    TowerHeight = value.TowerHeight;
                    SiteType = value.SiteType;
                    Technology = value.Technology;
                    Notes = value.Notes;
                }
            }
        }

        private string _siteId = "";
        public string SiteId { get => _siteId; set => SetProperty(ref _siteId, value); }

        private string _name = "";
        public string Name { get => _name; set => SetProperty(ref _name, value); }

        private double _latitude;
        public double Latitude { get => _latitude; set => SetProperty(ref _latitude, value); }

        private double _longitude;
        public double Longitude { get => _longitude; set => SetProperty(ref _longitude, value); }

        private double _towerHeight;
        public double TowerHeight { get => _towerHeight; set => SetProperty(ref _towerHeight, value); }

        private string _siteType = "";
        public string SiteType { get => _siteType; set => SetProperty(ref _siteType, value); }

        private string _technology = "";
        public string Technology { get => _technology; set => SetProperty(ref _technology, value); }

        private string _notes = "";
        public string Notes { get => _notes; set => SetProperty(ref _notes, value); }

        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand ClearCommand { get; }

        public SitesViewModel()
        {
            AddCommand = new RelayCommand(AddSite);
            UpdateCommand = new RelayCommand(UpdateSite);
            DeleteCommand = new RelayCommand(DeleteSite);
            ClearCommand = new RelayCommand(ClearForm);

            LoadSites();
        }

        private void LoadSites()
        {
            Sites = new ObservableCollection<Site>(DatabaseService.GetAllSites());
        }

        private void AddSite()
        {
            var site = new Site
            {
                SiteId = SiteId,
                Name = Name,
                Latitude = Latitude,
                Longitude = Longitude,
                TowerHeight = TowerHeight,
                SiteType = SiteType,
                Technology = Technology,
                Notes = Notes
            };
            DatabaseService.AddSite(site);
            LoadSites();
            ClearForm();
        }

        private void UpdateSite()
        {
            if (SelectedSite == null) return;
            var site = SelectedSite;
            site.SiteId = SiteId;
            site.Name = Name;
            site.Latitude = Latitude;
            site.Longitude = Longitude;
            site.TowerHeight = TowerHeight;
            site.SiteType = SiteType;
            site.Technology = Technology;
            site.Notes = Notes;

            DatabaseService.UpdateSite(site);
            LoadSites();
            ClearForm();
        }

        private void DeleteSite()
        {
            if (SelectedSite == null) return;
            DatabaseService.DeleteSite(SelectedSite.Id);
            LoadSites();
            ClearForm();
        }

        private void ClearForm()
        {
            SiteId = "";
            Name = "";
            Latitude = 0;
            Longitude = 0;
            TowerHeight = 0;
            SiteType = "";
            Technology = "";
            Notes = "";
            SelectedSite = null;
        }
    }
}
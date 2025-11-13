using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TP.Models;
using TP.Services;

namespace TP.ViewModels;

public class OfficesViewModel : BaseViewModel
{
    private readonly INavigationService _navigationService;
    private string _searchText = string.Empty;
    private string _selectedRegion = "Toutes";

    public OfficesViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        Title = "Bureaux";
        
        NavigateToHomeCommand = new Command(async () => await _navigationService.NavigateToAsync("//HomePage"));
        NavigateToExploreCommand = new Command(async () => await _navigationService.NavigateToAsync("//ExplorePage"));
        NavigateToAboutCommand = new Command(async () => await _navigationService.NavigateToAsync("//AboutPage"));
        
        SearchCommand = new Command(() => FilterOffices());
        
        InitializeData();
    }

    public ObservableCollection<Office> AllOffices { get; } = new();
    public ObservableCollection<Office> FilteredOffices { get; } = new();
    public ObservableCollection<string> Regions { get; } = new();

    public string SearchText
    {
        get => _searchText;
        set
        {
            SetProperty(ref _searchText, value);
            FilterOffices();
        }
    }

    public string SelectedRegion
    {
        get => _selectedRegion;
        set
        {
            SetProperty(ref _selectedRegion, value);
            FilterOffices();
        }
    }

    public ICommand NavigateToHomeCommand { get; }
    public ICommand NavigateToExploreCommand { get; }
    public ICommand NavigateToAboutCommand { get; }
    public ICommand SearchCommand { get; }

    private void InitializeData()
    {
        // Régions du Cameroun
        Regions.Add("Toutes");
        Regions.Add("Centre");
        Regions.Add("Littoral");
        Regions.Add("Ouest");
        Regions.Add("Nord-Ouest");
        Regions.Add("Sud-Ouest");
        Regions.Add("Est");
        Regions.Add("Adamaoua");
        Regions.Add("Nord");
        Regions.Add("Extrême-Nord");
        Regions.Add("Sud");

        // Bureaux administratifs principaux au Cameroun
        AllOffices.Add(new Office
        {
            Id = "1",
            Name = "Mairie de Yaoundé",
            Type = "Mairie",
            Address = "Avenue Kennedy, Yaoundé",
            City = "Yaoundé",
            Region = "Centre",
            Phone = "+237 222 22 00 00",
            Email = "mairie.yaounde@cameroun.cm",
            Schedule = "Lun-Ven: 7h30-15h30",
            Icon = "🏛️"
        });

        AllOffices.Add(new Office
        {
            Id = "2",
            Name = "Préfecture de Yaoundé",
            Type = "Préfecture",
            Address = "Boulevard du 20 Mai, Yaoundé",
            City = "Yaoundé",
            Region = "Centre",
            Phone = "+237 222 22 11 11",
            Schedule = "Lun-Ven: 8h-17h",
            Icon = "🏢"
        });

        AllOffices.Add(new Office
        {
            Id = "3",
            Name = "Mairie de Douala",
            Type = "Mairie",
            Address = "Avenue des Cocotiers, Douala",
            City = "Douala",
            Region = "Littoral",
            Phone = "+237 233 40 00 00",
            Email = "mairie.douala@cameroun.cm",
            Schedule = "Lun-Ven: 7h30-15h30",
            Icon = "🏛️"
        });

        AllOffices.Add(new Office
        {
            Id = "4",
            Name = "Préfecture de Douala",
            Type = "Préfecture",
            Address = "Boulevard de la République, Douala",
            City = "Douala",
            Region = "Littoral",
            Phone = "+237 233 40 11 11",
            Schedule = "Lun-Ven: 8h-17h",
            Icon = "🏢"
        });

        AllOffices.Add(new Office
        {
            Id = "5",
            Name = "Centre d'État Civil - Yaoundé",
            Type = "État Civil",
            Address = "Quartier Bastos, Yaoundé",
            City = "Yaoundé",
            Region = "Centre",
            Phone = "+237 222 22 33 33",
            Schedule = "Lun-Ven: 8h-15h",
            Icon = "📋"
        });

        AllOffices.Add(new Office
        {
            Id = "6",
            Name = "Direction Générale de la Documentation Nationale",
            Type = "CNI/Passeport",
            Address = "Boulevard du 20 Mai, Yaoundé",
            City = "Yaoundé",
            Region = "Centre",
            Phone = "+237 222 22 44 44",
            Schedule = "Lun-Ven: 8h-15h",
            Icon = "🛂"
        });

        AllOffices.Add(new Office
        {
            Id = "7",
            Name = "Mairie de Bafoussam",
            Type = "Mairie",
            Address = "Avenue de l'Indépendance, Bafoussam",
            City = "Bafoussam",
            Region = "Ouest",
            Phone = "+237 233 45 00 00",
            Schedule = "Lun-Ven: 7h30-15h30",
            Icon = "🏛️"
        });

        AllOffices.Add(new Office
        {
            Id = "8",
            Name = "Mairie de Garoua",
            Type = "Mairie",
            Address = "Avenue du Général de Gaulle, Garoua",
            City = "Garoua",
            Region = "Nord",
            Phone = "+237 227 22 00 00",
            Schedule = "Lun-Ven: 7h30-15h30",
            Icon = "🏛️"
        });

        FilteredOffices.Clear();
        foreach (var office in AllOffices)
        {
            FilteredOffices.Add(office);
        }
    }

    private void FilterOffices()
    {
        FilteredOffices.Clear();

        var filtered = AllOffices.AsEnumerable();

        // Filtrer par région
        if (SelectedRegion != "Toutes")
        {
            filtered = filtered.Where(o => o.Region == SelectedRegion);
        }

        // Filtrer par recherche
        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            var searchLower = SearchText.ToLower();
            filtered = filtered.Where(o =>
                o.Name.ToLower().Contains(searchLower) ||
                o.City.ToLower().Contains(searchLower) ||
                o.Type.ToLower().Contains(searchLower) ||
                o.Address.ToLower().Contains(searchLower));
        }

        foreach (var office in filtered)
        {
            FilteredOffices.Add(office);
        }
    }
}


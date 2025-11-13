using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TP.Models;
using TP.Services;

namespace TP.ViewModels;

public class ExploreViewModel : BaseViewModel
{
    private readonly INavigationService _navigationService;
    private readonly IProcedureService _procedureService;
    private string _searchText = string.Empty;
    private string _selectedCategoryId = "Toutes";

    public ExploreViewModel(INavigationService navigationService, IProcedureService procedureService)
    {
        _navigationService = navigationService;
        _procedureService = procedureService;
        Title = "Explorer";
        
        NavigateToHomeCommand = new Command(async () => await _navigationService.NavigateToAsync("//HomePage"));
        NavigateToOfficesCommand = new Command(async () => await _navigationService.NavigateToAsync("//OfficesPage"));
        NavigateToAboutCommand = new Command(async () => await _navigationService.NavigateToAsync("//AboutPage"));
        
        NavigateToProcedureCommand = new Command<Procedure>(async (procedure) =>
        {
            await _navigationService.NavigateToAsync($"//ProcedureDetailPage",
                new Dictionary<string, object> { { "ProcedureId", procedure.Id } });
        });

        NavigateToCategoryCommand = new Command<Category>(async (category) =>
        {
            await _navigationService.NavigateToAsync($"//CategoryPage",
                new Dictionary<string, object> { { "CategoryId", category.Id } });
        });
        
        InitializeData();
    }

    public ObservableCollection<Procedure> AllProcedures { get; } = new();
    public ObservableCollection<Procedure> FilteredProcedures { get; } = new();
    public ObservableCollection<Category> Categories { get; } = new();

    public string SearchText
    {
        get => _searchText;
        set
        {
            SetProperty(ref _searchText, value);
            FilterProcedures();
        }
    }

    public string SelectedCategoryId
    {
        get => _selectedCategoryId;
        set
        {
            SetProperty(ref _selectedCategoryId, value);
            FilterProcedures();
        }
    }

    public ICommand NavigateToHomeCommand { get; }
    public ICommand NavigateToOfficesCommand { get; }
    public ICommand NavigateToAboutCommand { get; }
    public ICommand NavigateToProcedureCommand { get; }
    public ICommand NavigateToCategoryCommand { get; }

    private void InitializeData()
    {
        // Catégories
        Categories.Add(new Category { Id = "Toutes", Name = "Toutes", Icon = "📁", ProcedureCount = 0 });
        Categories.Add(new Category { Id = "1", Name = "État Civil", Icon = "📋", ProcedureCount = 8 });
        Categories.Add(new Category { Id = "2", Name = "Entreprises", Icon = "🏢", ProcedureCount = 12 });
        Categories.Add(new Category { Id = "3", Name = "Fiscalité", Icon = "🧮", ProcedureCount = 3 });
        Categories.Add(new Category { Id = "4", Name = "Éducation", Icon = "🎓", ProcedureCount = 6 });
        Categories.Add(new Category { Id = "5", Name = "Transport", Icon = "🚗", ProcedureCount = 7 });

        // Charger toutes les procédures depuis le service
        var allProcedures = _procedureService.GetAllProcedures();
        AllProcedures.Clear();
        foreach (var procedure in allProcedures)
        {
            AllProcedures.Add(procedure);
        }

        FilteredProcedures.Clear();
        foreach (var procedure in AllProcedures)
        {
            FilteredProcedures.Add(procedure);
        }
    }

    private void FilterProcedures()
    {
        FilteredProcedures.Clear();

        var filtered = AllProcedures.AsEnumerable();

        // Filtrer par catégorie
        if (SelectedCategoryId != "Toutes")
        {
            filtered = filtered.Where(p => p.CategoryId == SelectedCategoryId);
        }

        // Filtrer par recherche
        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            var searchLower = SearchText.ToLower();
            filtered = filtered.Where(p =>
                p.Title.ToLower().Contains(searchLower) ||
                p.Description.ToLower().Contains(searchLower));
        }

        foreach (var procedure in filtered)
        {
            FilteredProcedures.Add(procedure);
        }
    }
}


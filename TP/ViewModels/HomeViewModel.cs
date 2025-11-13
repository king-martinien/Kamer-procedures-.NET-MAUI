using System.Collections.ObjectModel;
using System.Windows.Input;
using TP.Models;
using TP.Services;

namespace TP.ViewModels;

public class HomeViewModel : BaseViewModel
{
    private readonly INavigationService _navigationService;
    private readonly IProcedureService _procedureService;

    public HomeViewModel(INavigationService navigationService, IProcedureService procedureService)
    {
        _navigationService = navigationService;
        _procedureService = procedureService;
        Title = "Procédures Administratives";

        InitializeData();
    }

    public Statistics Statistics { get; private set; } = new();
    public ObservableCollection<Procedure> PopularProcedures { get; } = new();
    public ObservableCollection<Category> Categories { get; } = new();

    public ICommand NavigateToProcedureCommand { get; private set; } = null!;
    public ICommand NavigateToCategoryCommand { get; private set; } = null!;
    public ICommand NavigateToOfficesCommand { get; private set; } = null!;

    private void InitializeData()
    {
        // Statistics
        Statistics = new Statistics
        {
            ProcedureCount = 53,
            OfficeCount = 15,
            Assistance = "24/7"
        };

        // Popular Procedures
        var popularProcedures = _procedureService.GetPopularProcedures();
        foreach (var procedure in popularProcedures)
        {
            PopularProcedures.Add(procedure);
        }

        // Categories
        Categories.Add(new Category
        {
            Id = "1",
            Name = "État Civil",
            Icon = "📋",
            ProcedureCount = 8
        });

        Categories.Add(new Category
        {
            Id = "2",
            Name = "Entreprises",
            Icon = "🏢",
            ProcedureCount = 12
        });

        Categories.Add(new Category
        {
            Id = "3",
            Name = "Fiscalité",
            Icon = "🧮",
            ProcedureCount = 3
        });

        Categories.Add(new Category
        {
            Id = "4",
            Name = "Éducation",
            Icon = "🎓",
            ProcedureCount = 6
        });

        Categories.Add(new Category
        {
            Id = "5",
            Name = "Transport",
            Icon = "🚗",
            ProcedureCount = 7
        });

        Categories.Add(new Category
        {
            Id = "6",
            Name = "Autres",
            Icon = "⋯",
            ProcedureCount = 17
        });

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

        NavigateToOfficesCommand = new Command(async () =>
        {
            await _navigationService.NavigateToAsync("//OfficesPage");
        });
    }
}

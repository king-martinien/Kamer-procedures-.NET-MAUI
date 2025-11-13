using System.Collections.ObjectModel;
using System.Windows.Input;
using TP.Models;
using TP.Services;

namespace TP.ViewModels;

public class CategoryViewModel : BaseViewModel
{
    private readonly INavigationService _navigationService;
    private readonly IProcedureService _procedureService;
    private Category _category = null!;
    private string _categoryId = string.Empty;

    public CategoryViewModel(INavigationService navigationService, IProcedureService procedureService)
    {
        _navigationService = navigationService;
        _procedureService = procedureService;
        BackCommand = new Command(async () => await _navigationService.NavigateToAsync("//HomePage"));
        NavigateToProcedureCommand = new Command<Procedure>(async (procedure) =>
        {
            await _navigationService.NavigateToAsync($"//ProcedureDetailPage",
                new Dictionary<string, object> { { "ProcedureId", procedure.Id } });
        });
        
        // Initialize with default category
        Category = new Category { Id = "", Name = "", Icon = "", ProcedureCount = 0 };
    }

    public void Initialize(string categoryId)
    {
        _categoryId = categoryId;
        LoadCategoryData();
    }

    public Category Category
    {
        get => _category;
        set => SetProperty(ref _category, value);
    }

    public ObservableCollection<Procedure> Procedures { get; } = new();

    public ICommand BackCommand { get; }
    public ICommand NavigateToProcedureCommand { get; }

    private void LoadCategoryData()
    {
        // Définir les catégories
        var categories = new Dictionary<string, Category>
        {
            { "1", new Category { Id = "1", Name = "État Civil", Icon = "📋", ProcedureCount = 8 } },
            { "2", new Category { Id = "2", Name = "Entreprises", Icon = "🏢", ProcedureCount = 12 } },
            { "3", new Category { Id = "3", Name = "Fiscalité", Icon = "🧮", ProcedureCount = 3 } },
            { "4", new Category { Id = "4", Name = "Éducation", Icon = "🎓", ProcedureCount = 6 } },
            { "5", new Category { Id = "5", Name = "Transport", Icon = "🚗", ProcedureCount = 7 } }
        };

        if (categories.ContainsKey(_categoryId))
        {
            Category = categories[_categoryId];
        }

        // Charger les procédures de la catégorie depuis le service
        Procedures.Clear();
        var categoryProcedures = _procedureService.GetProceduresByCategory(_categoryId);
        foreach (var procedure in categoryProcedures)
        {
            Procedures.Add(procedure);
        }
    }
}


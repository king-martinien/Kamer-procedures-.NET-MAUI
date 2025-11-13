using System.Windows.Input;
using TP.Models;
using TP.Services;

namespace TP.ViewModels;

public class ProcedureDetailViewModel : BaseViewModel
{
    private readonly INavigationService _navigationService;
    private readonly IProcedureService _procedureService;
    private Procedure _procedure = null!;
    private string _selectedTab = "Nouvelle demande";
    private int _completedSteps = 0;

    public ProcedureDetailViewModel(INavigationService navigationService, IProcedureService procedureService)
    {
        _navigationService = navigationService;
        _procedureService = procedureService;
        BackCommand = new Command(async () => await ExecuteBackCommand());
        SelectTabCommand = new Command<string>(SelectTab);
        
        // Initialize with default procedure
        LoadProcedure("1");
    }

    private async Task ExecuteBackCommand()
    {
        try
        {
            // Naviguer directement vers HomePage (plus fiable avec Shell)
            await _navigationService.NavigateToAsync("//HomePage");
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Erreur lors de la navigation retour: {ex.Message}");
            // Essayer la navigation retour standard en fallback
            try
            {
                await _navigationService.NavigateBackAsync();
            }
            catch
            {
                // Si les deux échouent, on ne peut rien faire
            }
        }
    }

    public void Initialize(string procedureId)
    {
        if (!string.IsNullOrEmpty(procedureId))
        {
            LoadProcedure(procedureId);
        }
    }

    public Procedure Procedure
    {
        get => _procedure;
        set => SetProperty(ref _procedure, value);
    }

    public string SelectedTab
    {
        get => _selectedTab;
        set => SetProperty(ref _selectedTab, value);
    }

    public int CompletedSteps
    {
        get => _completedSteps;
        set
        {
            SetProperty(ref _completedSteps, value);
            OnPropertyChanged(nameof(ProgressPercentage));
            OnPropertyChanged(nameof(ProgressText));
        }
    }

    public double ProgressPercentage => Procedure?.StepCount > 0 
        ? (double)CompletedSteps / Procedure.StepCount 
        : 0;

    public string ProgressText => $"{CompletedSteps}/{Procedure?.StepCount ?? 0} étapes - {(ProgressPercentage * 100):F0}% complété";

    public ICommand BackCommand { get; }
    public ICommand SelectTabCommand { get; }

    public void LoadProcedure(string procedureId)
    {
        var procedure = _procedureService.GetProcedureById(procedureId);
        if (procedure != null)
        {
            Procedure = procedure;
            // Réinitialiser les étapes complétées quand on change de procédure
            CompletedSteps = 0;
        }
        else
        {
            // Fallback si la procédure n'est pas trouvée
            Procedure = new Procedure
            {
                Id = procedureId,
                Title = "Procédure non trouvée",
                Description = "Les détails de cette procédure ne sont pas disponibles.",
                Icon = "📄",
                Duration = "N/A",
                Cost = "N/A",
                Difficulty = "N/A",
                StepCount = 0
            };
        }
    }

    private void SelectTab(string tab)
    {
        SelectedTab = tab;
    }
}


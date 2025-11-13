using System.Collections.ObjectModel;
using System.Windows.Input;
using TP.Models;
using TP.Services;

namespace TP.ViewModels;

public class OnboardingViewModel : BaseViewModel
{
    private readonly INavigationService _navigationService;
    private readonly IStorageService _storageService;
    private int _currentIndex = 0;

    public OnboardingViewModel(INavigationService navigationService, IStorageService storageService)
    {
        _navigationService = navigationService;
        _storageService = storageService;
        Title = "Onboarding";

        NextCommand = new Command(ExecuteNextCommand);
        SkipCommand = new Command(ExecuteSkipCommand);

        InitializeOnboardingItems();
    }

    public ObservableCollection<OnboardingItem> OnboardingItems { get; } = new();

    public int CurrentIndex
    {
        get => _currentIndex;
        set
        {
            if (SetProperty(ref _currentIndex, value))
            {
                OnPropertyChanged(nameof(CurrentItem));
            }
        }
    }

    public OnboardingItem CurrentItem => OnboardingItems.Count > 0 ? OnboardingItems[CurrentIndex] : OnboardingItems.FirstOrDefault() ?? new OnboardingItem();

    public bool CanExecuteNext => true; // Always enabled to allow completion on last screen

    public ICommand NextCommand { get; }
    public ICommand SkipCommand { get; }

    private void InitializeOnboardingItems()
    {
        OnboardingItems.Add(new OnboardingItem
        {
            Title = "Simplifiez vos démarches",
            Subtitle = "Simplifiez vos démarches administratives au Cameroun avec notre guide complet et actualisé",
            Icon = "📄",
            BackgroundColor = Color.FromArgb("#1A5D4A"),
            ButtonColor = Color.FromArgb("#0D4A3A"),
            ButtonText = "Suivant"
        });

        OnboardingItems.Add(new OnboardingItem
        {
            Title = "Toutes les procédures",
            Subtitle = "Plus de 20 démarches",
            Description = "Découvrez toutes les procédures administratives avec les documents requis, délais et coûts",
            Icon = "🔍",
            BackgroundColor = Color.FromArgb("#C62828"),
            ButtonColor = Color.FromArgb("#8B1A1A"),
            ButtonText = "Suivant"
        });

        OnboardingItems.Add(new OnboardingItem
        {
            Title = "Bureaux proches",
            Subtitle = "Localisez facilement",
            Description = "Trouvez les bureaux administratifs les plus proches de votre position avec horaires et contacts.",
            Icon = "📍",
            BackgroundColor = Color.FromArgb("#F9A825"),
            ButtonColor = Color.FromArgb("#B87A1A"),
            ButtonText = "Commencer"
        });
    }

    private async void ExecuteNextCommand()
    {
        if (CurrentIndex < OnboardingItems.Count - 1)
        {
            CurrentIndex++;
        }
        else
        {
            // On est sur le dernier écran, compléter l'onboarding
            await CompleteOnboarding();
        }
    }

    private async void ExecuteSkipCommand()
    {
        await CompleteOnboarding();
    }

    private async Task CompleteOnboarding()
    {
        try
        {
            // Sauvegarder que l'onboarding est complété
            await _storageService.SetOnboardingCompletedAsync(true);
            
            // Naviguer vers la page d'accueil
            await _navigationService.NavigateToAsync("//HomePage");
        }
        catch (Exception ex)
        {
            // Log l'erreur si nécessaire
            System.Diagnostics.Debug.WriteLine($"Erreur lors de la complétion de l'onboarding: {ex.Message}");
        }
    }
}


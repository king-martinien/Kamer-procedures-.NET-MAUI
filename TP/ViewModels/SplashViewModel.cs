using TP.Services;

namespace TP.ViewModels;

public class SplashViewModel : BaseViewModel
{
    private readonly INavigationService _navigationService;
    private readonly IStorageService _storageService;

    public SplashViewModel(INavigationService navigationService, IStorageService storageService)
    {
        _navigationService = navigationService;
        _storageService = storageService;
        Title = "Splash";
    }

    public async Task InitializeAsync()
    {
        // Simuler un délai pour le splash screen
        await Task.Delay(2000);

        try
        {
            // Vérifier si l'onboarding a été complété
            var onboardingCompleted = await _storageService.GetOnboardingCompletedAsync();

            if (onboardingCompleted)
            {
                // L'onboarding a déjà été complété, aller directement à la page d'accueil
                await _navigationService.NavigateToAsync("//HomePage");
            }
            else
            {
                // Première utilisation, afficher l'onboarding
                await _navigationService.NavigateToAsync("//OnboardingPage");
            }
        }
        catch (Exception ex)
        {
            // En cas d'erreur, afficher l'onboarding par défaut
            System.Diagnostics.Debug.WriteLine($"Erreur lors de la vérification de l'onboarding: {ex.Message}");
            await _navigationService.NavigateToAsync("//OnboardingPage");
        }
    }
}


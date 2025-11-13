using Microsoft.Maui.Storage;

namespace TP.Services;

public class StorageService : IStorageService
{
    private const string OnboardingCompletedKey = "OnboardingCompleted";

    public async Task<bool> GetOnboardingCompletedAsync()
    {
        return await Task.FromResult(Preferences.Get(OnboardingCompletedKey, false));
    }

    public async Task SetOnboardingCompletedAsync(bool value)
    {
        Preferences.Set(OnboardingCompletedKey, value);
        System.Diagnostics.Debug.WriteLine($"Onboarding complété sauvegardé: {value}");
        await Task.CompletedTask;
    }
}


namespace TP.Services;

public interface IStorageService
{
    Task<bool> GetOnboardingCompletedAsync();
    Task SetOnboardingCompletedAsync(bool value);
}


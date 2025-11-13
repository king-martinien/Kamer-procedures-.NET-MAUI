using System.Windows.Input;
using TP.Services;

namespace TP.ViewModels;

public class AboutViewModel : BaseViewModel
{
    private readonly INavigationService _navigationService;

    public AboutViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        Title = "A Propos";
        
        NavigateToHomeCommand = new Command(async () => await _navigationService.NavigateToAsync("//HomePage"));
        NavigateToExploreCommand = new Command(async () => await _navigationService.NavigateToAsync("//ExplorePage"));
        NavigateToOfficesCommand = new Command(async () => await _navigationService.NavigateToAsync("//OfficesPage"));
    }

    public string Version => "v1.0.0 (2025)";
    public string Email => "martinienfokoue@gmail.com";
    public string Phone => "+237 657 263 559";

    public ICommand NavigateToHomeCommand { get; }
    public ICommand NavigateToExploreCommand { get; }
    public ICommand NavigateToOfficesCommand { get; }
}


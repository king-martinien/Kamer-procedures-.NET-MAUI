using TP.Services;
using TP.ViewModels;

namespace TP.Views;

public partial class HomePage : ContentPage
{
    private readonly INavigationService? _navigationService;

    public HomePage(HomeViewModel viewModel, INavigationService navigationService)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _navigationService = navigationService;
    }

    private void OnSeeAllTapped(object? sender, EventArgs e)
    {
        // TODO: Navigate to all procedures page
    }

    private async void OnAboutTapped(object? sender, EventArgs e)
    {
        if (_navigationService != null)
        {
            await _navigationService.NavigateToAsync("//AboutPage");
        }
    }

    private async void OnExploreTapped(object? sender, EventArgs e)
    {
        if (_navigationService != null)
        {
            await _navigationService.NavigateToAsync("//ExplorePage");
        }
    }

    private async void OnOfficesTapped(object? sender, EventArgs e)
    {
        if (_navigationService != null)
        {
            await _navigationService.NavigateToAsync("//OfficesPage");
        }
    }
}

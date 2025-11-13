using TP.ViewModels;

namespace TP.Views;

public partial class SplashPage : ContentPage
{
    public SplashPage(SplashViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        
        if (BindingContext is SplashViewModel viewModel)
        {
            await viewModel.InitializeAsync();
        }
    }
}


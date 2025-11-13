using TP.ViewModels;

namespace TP.Views;

public partial class ExplorePage : ContentPage
{
    public ExplorePage(ExploreViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}


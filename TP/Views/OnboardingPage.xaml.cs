using TP.Models;
using TP.ViewModels;

namespace TP.Views;

public partial class OnboardingPage : ContentPage
{
    private OnboardingViewModel? _viewModel;

    public OnboardingPage(OnboardingViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
        
        OnboardingCarousel.CurrentItemChanged += OnCurrentItemChanged;
        viewModel.PropertyChanged += ViewModel_PropertyChanged;
        UpdateButton();
    }

    private void ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(OnboardingViewModel.CurrentItem) || 
            e.PropertyName == nameof(OnboardingViewModel.CurrentIndex))
        {
            UpdateButton();
            
            // Synchronize CarouselView with ViewModel
            if (e.PropertyName == nameof(OnboardingViewModel.CurrentIndex) && 
                _viewModel != null && 
                _viewModel.CurrentIndex >= 0 && 
                _viewModel.CurrentIndex < _viewModel.OnboardingItems.Count)
            {
                var targetItem = _viewModel.OnboardingItems[_viewModel.CurrentIndex];
                if (OnboardingCarousel.CurrentItem != targetItem)
                {
                    OnboardingCarousel.CurrentItem = targetItem;
                }
            }
        }
    }

    private void OnCurrentItemChanged(object? sender, CurrentItemChangedEventArgs e)
    {
        if (_viewModel != null && e.CurrentItem is OnboardingItem item)
        {
            var index = _viewModel.OnboardingItems.IndexOf(item);
            if (index >= 0 && index != _viewModel.CurrentIndex)
            {
                _viewModel.CurrentIndex = index;
            }
        }
    }

    private void UpdateButton()
    {
        if (_viewModel?.CurrentItem != null)
        {
            NextButton.Text = _viewModel.CurrentItem.ButtonText;
            NextButton.BackgroundColor = _viewModel.CurrentItem.ButtonColor;
            NextButton.IsEnabled = true; // S'assurer que le bouton est toujours activé
        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        if (_viewModel != null)
        {
            _viewModel.PropertyChanged -= ViewModel_PropertyChanged;
        }
    }
}


using Microsoft.Maui.Controls.Shapes;
using TP.ViewModels;

namespace TP.Views;

public partial class OfficesPage : ContentPage
{
    private OfficesViewModel? _viewModel;
    private Dictionary<string, Border> _regionFilters = new();

    public OfficesPage(OfficesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
        
        viewModel.PropertyChanged += ViewModel_PropertyChanged;
        LoadRegionFilters();
    }

    private void ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(OfficesViewModel.SelectedRegion))
        {
            UpdateRegionFilters();
        }
    }

    private void LoadRegionFilters()
    {
        if (_viewModel == null) return;

        RegionsStackLayout.Children.Clear();
        _regionFilters.Clear();

        foreach (var region in _viewModel.Regions)
        {
            var border = new Border
            {
                BackgroundColor = region == _viewModel.SelectedRegion ? Color.FromArgb("#1A5D4A") : Colors.White,
                StrokeThickness = 1,
                Stroke = Color.FromArgb("#E0E0E0"),
                Padding = new Thickness(15, 8),
                StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(20) }
            };

            var label = new Label
            {
                Text = region,
                FontSize = 14,
                FontAttributes = FontAttributes.Bold,
                TextColor = region == _viewModel.SelectedRegion ? Colors.White : Colors.Black
            };

            border.Content = label;

            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += (s, e) => OnRegionFilterTapped(region);
            border.GestureRecognizers.Add(tapGesture);

            RegionsStackLayout.Children.Add(border);
            _regionFilters[region] = border;
        }
    }

    private void UpdateRegionFilters()
    {
        if (_viewModel == null) return;

        foreach (var kvp in _regionFilters)
        {
            var region = kvp.Key;
            var border = kvp.Value;
            var isSelected = region == _viewModel.SelectedRegion;

            border.BackgroundColor = isSelected ? Color.FromArgb("#1A5D4A") : Colors.White;
            
            if (border.Content is Label label)
            {
                label.TextColor = isSelected ? Colors.White : Colors.Black;
            }
        }
    }

    private void OnRegionFilterTapped(string region)
    {
        if (_viewModel != null)
        {
            _viewModel.SelectedRegion = region;
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


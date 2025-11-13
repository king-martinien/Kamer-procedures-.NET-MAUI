using TP.ViewModels;

namespace TP.Views;

[QueryProperty(nameof(ProcedureId), "ProcedureId")]
public partial class ProcedureDetailPage : ContentPage
{
    private ProcedureDetailViewModel? _viewModel;
    private string _procedureId = string.Empty;

    public string ProcedureId
    {
        get => _procedureId;
        set
        {
            _procedureId = value;
            if (!string.IsNullOrEmpty(_procedureId) && _viewModel != null)
            {
                _viewModel.Initialize(_procedureId);
            }
        }
    }

    public ProcedureDetailPage(ProcedureDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
        
        viewModel.PropertyChanged += ViewModel_PropertyChanged;
        UpdateTabs();
        
        if (!string.IsNullOrEmpty(_procedureId))
        {
            _viewModel.Initialize(_procedureId);
        }
    }

    private void ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ProcedureDetailViewModel.SelectedTab))
        {
            UpdateTabs();
        }
    }

    private void UpdateTabs()
    {
        if (_viewModel == null) return;

        if (_viewModel.SelectedTab == "Nouvelle demande")
        {
            NewRequestTab.BackgroundColor = Color.FromArgb("#1A5D4A");
            RenewalTab.BackgroundColor = Color.FromArgb("#E0E0E0");
            NewRequestLabel.TextColor = Colors.White;
            RenewalLabel.TextColor = Color.FromArgb("#666666");
        }
        else
        {
            NewRequestTab.BackgroundColor = Color.FromArgb("#E0E0E0");
            RenewalTab.BackgroundColor = Color.FromArgb("#1A5D4A");
            NewRequestLabel.TextColor = Color.FromArgb("#666666");
            RenewalLabel.TextColor = Colors.White;
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


using TP.ViewModels;

namespace TP.Views;

[QueryProperty(nameof(CategoryId), "CategoryId")]
public partial class CategoryPage : ContentPage
{
    private CategoryViewModel? _viewModel;
    private string _categoryId = string.Empty;

    public string CategoryId
    {
        get => _categoryId;
        set
        {
            _categoryId = value;
            if (!string.IsNullOrEmpty(_categoryId) && _viewModel != null)
            {
                _viewModel.Initialize(_categoryId);
            }
        }
    }

    public CategoryPage(CategoryViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
        
        if (!string.IsNullOrEmpty(_categoryId))
        {
            _viewModel.Initialize(_categoryId);
        }
    }
}


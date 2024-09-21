using Maui.ViewModels;

namespace Maui.Pages;

public partial class EditProductPage : ContentPage
{
    private readonly EditProductViewModel _viewModel;
    public EditProductPage(EditProductViewModel viewModel, int productId)
	{
		InitializeComponent();

        _viewModel = viewModel;
        _viewModel.LoadProduct(productId);
        BindingContext = _viewModel;
    }
}
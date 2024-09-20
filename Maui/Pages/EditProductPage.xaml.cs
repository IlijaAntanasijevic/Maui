using Maui.ViewModels;

namespace Maui.Pages;

public partial class EditProductPage : ContentPage
{
	public EditProductPage(int productId)
	{
		InitializeComponent();


        var viewModel = new EditProductViewModel();
        viewModel.LoadProduct(productId);

        this.BindingContext = viewModel;
    }
}
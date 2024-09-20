using Maui.Common;
using Maui.DTO;
using Maui.ViewModels;

namespace Maui.Pages;

public partial class ProductPage : ContentPage
{
    public ProductPage(int productId)
	{
		InitializeComponent();

        var viewModel = new ProductViewModel();
        viewModel.LoadProduct(productId);

        this.BindingContext = viewModel;
    }

}
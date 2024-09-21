using Maui.Common;
using Maui.DTO;
using Maui.ViewModels;

namespace Maui.Pages;

public partial class ProductPage : ContentPage
{
    private readonly ProductViewModel _viewModel;
    public ProductPage(ProductViewModel viewModel, int productId)
    {
        InitializeComponent();
        _viewModel = viewModel;

        _viewModel.LoadProduct(productId);
        BindingContext = _viewModel;
    }

}
using Maui.ViewModels;

namespace Maui.Pages;

public partial class ProductsPage : ContentPage
{
    private readonly ProductsViewModel _viewModel;
	public ProductsPage()
	{
		InitializeComponent();
        _viewModel = new ProductsViewModel();
        BindingContext = _viewModel;
	}

    private async void Product_View(object sender, EventArgs e)
    {
        var button = sender as Button;
        var productId = (int)button.CommandParameter;
        //await App.Current.MainPage.Navigation.PushAsync(new ProductPage(productId));
        //await Navigation.PushAsync(new NavigationPage(new ProductPage(productId)));

        var productPage = ActivatorUtilities.CreateInstance<ProductPage>(App.Services, productId);
        await Navigation.PushAsync(productPage);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadProducts();
    }

}
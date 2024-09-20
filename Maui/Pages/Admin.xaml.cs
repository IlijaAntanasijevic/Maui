using Maui.ViewModels;

namespace Maui.Pages;

public partial class Admin : ContentPage
{
    private readonly AdminViewModel _viewModel;
    public Admin()
    {
        InitializeComponent();
        _viewModel = new AdminViewModel();
        this.BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadProducts();  
    }

    private async void Add(object sender, EventArgs e)
    {

        await Navigation.PushAsync(new NavigationPage(new AddProductPage()));
    }

    private async void Edit(object sender, EventArgs e)
    {
        var button = sender as Button;
        var productId = (int)button.CommandParameter;
        await Navigation.PushAsync(new NavigationPage(new EditProductPage(productId)));
    }

    private async void ChangeEmail(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NavigationPage(new ChangeEmail()));
    }
}
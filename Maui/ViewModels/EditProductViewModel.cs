using Maui.Common;
using Maui.DTO;
using Maui.Services.Interfaces;
using RestSharp;
using System.Windows.Input;


namespace Maui.ViewModels
{
    public class EditProductViewModel : ProductFormViewModel
    {
        private readonly IProductService _productService;
        public EditProductViewModel(IProductService productService)
            :base()
        {
            _productService = productService;
            EditProductCommand = new Command(Edit);
            DeleteProductCommand = new Command(Delete);
        }

        public ICommand EditProductCommand { get; }
        public ICommand DeleteProductCommand { get; }


        public void LoadProduct(int productId)
        {

            var product = _productService.GetProduct(productId);
            if (product != null)
            {
                this.SetProductData(product);
            }
            else
            {
                Product.Error = "Server error!";
                Product.Value = null;
            }
        }
        public async void Edit()
        {
            var product = new SingleProductDto
            {
                Id = Product.Value.Id,
                Name = Name.Value,
                Price = Price.Value,
                TotalQuantity = TotalQuantity.Value,
                Details = Details.Value
            };
            bool isSuccessful = _productService.EditProduct(product);

            if (isSuccessful)
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                Product.Error = "Server error!";
            }
        }

        public async void Delete()
        {
            int Id = Product.Value.Id;
            _productService.DeleteProduct(Id);
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}

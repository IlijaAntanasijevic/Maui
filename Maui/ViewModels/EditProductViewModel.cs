using Maui.Common;
using Maui.DTO;
using Maui.Pages;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

namespace Maui.ViewModels
{
    public class EditProductViewModel : ProductFormViewModel
    {
        public MProp<int> Id {  get; set; } = new MProp<int>();
        public MProp<SingleProductDto> Product { get; set; } = new MProp<SingleProductDto>();

        public EditProductViewModel()
            :base()
        {
            EditProductCommand = new Command(Edit);
            DeleteProductCommand = new Command(Delete);
        }

        public ICommand EditProductCommand { get; }
        public ICommand DeleteProductCommand { get; }


        public void LoadProduct(int productId)
        {
            if (productId <= 0)
            {
                Product.Error = "Server error!";
                Product.Value = null;
                return;
            }

            Id.Value = productId;

            RestRequest request = new RestRequest($"products/show/{productId}");
            var response = Api.Client.Execute<SingleProductDto>(request);
            if (response.IsSuccessful)
            {

                response.Data.Path = $"{Api.BaseImageUrl}{response.Data.Path}";
                var productData = response.Data;

                Product.Value = productData;
                Name.Value = productData.Name;
                Price.Value = productData.Price;
                TotalQuantity.Value = productData.TotalQuantity;
                Details.Value = productData.Details;
                FileSource.Value = "/";

                Product.Error = null;
            }
            else
            {
                Product.Error = "Server error!";
                Product.Value = null;

            }
        }

        public async void Edit()
        {
            string name = Name.Value;
            decimal price = Price.Value;
            int totalQuantity = TotalQuantity.Value;
            string details = Details.Value;
            int Id = this.Id.Value;

            var request = new RestRequest("products", Method.Put);
            request.AddJsonBody(new { name, price, totalQuantity, details, Id});

            RestResponse response = Api.Client.Execute(request);

            if (response.IsSuccessful)
            {
                await Application.Current.MainPage.Navigation.PopAsync();

                //App.Current.MainPage = new Admin();
            }
        }

        public async void Delete()
        {
            int Id = this.Id.Value;
            var request = new RestRequest($"products/{Id}", Method.Delete);

            RestResponse response = Api.Client.Execute(request);

            if (response.IsSuccessful)
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            }

        }
    }
}

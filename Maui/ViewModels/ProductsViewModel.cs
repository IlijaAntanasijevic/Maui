using Maui.Common;
using Maui.DTO;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.ViewModels
{
    public class ProductsViewModel
    {
        public MProp<string> Keyword { get; set; } = new MProp<string>();
        public ObservableCollection<ProductDto> Products { get; set; } = new ObservableCollection<ProductDto>();
        public MProp<bool> HasProducts { get; set; } = new MProp<bool>();

        public ProductsViewModel()
        {
            Keyword.OnChange = LoadProducts;

            LoadProducts();
        }

        public void LoadProducts()
        {
            Products.Clear();
            RestRequest request = string.IsNullOrEmpty(Keyword.Value) ? new RestRequest("products") :
                                                                        new RestRequest($"products/{Keyword.Value}");
            var response = Api.Client.Execute<ObservableCollection<ProductDto>>(request);
            if (response.IsSuccessful)
            {
                var products = response.Data.Select(x =>
                {
                    x.Path = $"{Api.BaseImageUrl}{x.Path}";
                    return x;
                }).ToList();

                foreach (var product in products)
                {
                    Products.Add(product);
                }
                HasProducts.Error = Products.Any() ? "" : "No products found.";
            }
            else
            {
                Products.Clear();
                HasProducts.Error = "Server error!";
            }
        }
    }
}

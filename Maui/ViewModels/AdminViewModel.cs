using Maui.Common;
using Maui.DTO;
using Maui.Validators;
using Microsoft.Maui;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Maui.ViewModels
{
    
    public class AdminViewModel
    {
     
        public ObservableCollection<ProductDto> Products { get; set; } = new ObservableCollection<ProductDto>();

        public AdminViewModel()
        {
            LoadProducts();
        }


        public void LoadProducts()
        {
            Products.Clear();
            RestRequest request = new RestRequest("products");
                                                                       
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
            }
            else
            {
                Products.Clear();
            }
        }
    }
}

using Maui.Common;
using Maui.DTO;
using Maui.Services.Interfaces;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Maui.Services.Implementation
{
    public class ProductService : IProductService
    {
        public SingleProductDto GetProduct(int productId)
        {
            RestRequest request = new RestRequest($"products/show/{productId}");
            var response = Api.Client.Execute<SingleProductDto>(request);
            if (response.IsSuccessful)
            {
                response.Data.Path = response.Data.Path.SetImagePath();
                return response.Data;
            }
            return null;
        }

        public bool EditProduct(SingleProductDto product)
        {
            var request = new RestRequest("products", Method.Put);
            request.AddJsonBody(new
            {
                name = product.Name,
                price = product.Price,
                totalQuantity = product.TotalQuantity,
                details = product.Details,
                Id = product.Id
            });

            RestResponse response = Api.Client.Execute(request);
            return response.IsSuccessful;
        }

        public void DeleteProduct(int id)
        {
            var request = new RestRequest($"products/{id}", Method.Delete);
            RestResponse response = Api.Client.Execute(request);
            //return response.IsSuccessful;
        }
    }
}

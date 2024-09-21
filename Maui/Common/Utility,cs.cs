using Maui.DTO;
using Maui.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.Common
{
    public static class Utility
    {
        public static bool IsRegistration { get; set; } = false;

        public static string GetMimeType(string fileName)
        {
            var mimeTypes = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)
             {
                    { ".png", "image/png" },
                    { ".jpg", "image/jpeg" },
                    { ".jpeg", "image/jpeg" }
            };

            var extension = Path.GetExtension(fileName).ToLowerInvariant();

            if (mimeTypes.ContainsKey(extension))
            {
                return mimeTypes[extension];
            }

            return null;
        }
        
        public static string SetImagePath(this string imagePath)
        {
           return Api.BaseImageUrl + imagePath;
        }

        public static void SetProductData(this ProductFormViewModel viewModel, SingleProductDto product)
        {
            viewModel.Product.Value = product;
            viewModel.Name.Value = product.Name;
            viewModel.Price.Value = product.Price;
            viewModel.TotalQuantity.Value = product.TotalQuantity;
            viewModel.Details.Value = product.Details;
            viewModel.FileSource.Value = "/";
            viewModel.Product.Value.Id = product.Id;

        }
    }
}

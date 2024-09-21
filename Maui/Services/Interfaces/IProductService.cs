using Maui.Common;
using Maui.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.Services.Interfaces
{
    public interface IProductService
    {
        SingleProductDto GetProduct(int id);

        bool EditProduct(SingleProductDto product);

        void DeleteProduct(int id);
    }
}

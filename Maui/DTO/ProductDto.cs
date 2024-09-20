using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.DTO
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TotalQuantity { get; set; }
        public decimal Price { get; set; }
        public string Path {  get; set; }
    }

    public class SingleProductDto : ProductDto
    {
        public string Details { get; set; }
    }
}

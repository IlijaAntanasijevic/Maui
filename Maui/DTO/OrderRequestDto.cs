using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.DTO
{
    public class OrderRequestDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Quantity { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        //public int ProductId { get; set; }
        public SingleProductDto Product { get; set; }
    }

    public class ProductAvailabilityRequestDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Quantity { get; set; }
        public int Id { get; set; }
    }
}

using Maui.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.Services.Interfaces
{
    public interface IOrderService
    {
        Task<bool> PlaceOrder(OrderRequestDto order);

        Task<string> CheckProductAvailability(ProductAvailabilityRequestDto productAvailability);
    }
}

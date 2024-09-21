using Maui.DTO;
using Maui.Services.Interfaces;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.Services.Implementation
{
    public class OrderService : IOrderService
    {
        public async Task<bool> PlaceOrder(OrderRequestDto order)
        {
            var request = new RestRequest("orders", Method.Post);
            request.AddJsonBody(new
            {
                order.StartDate,
                order.EndDate,
                order.Quantity,
                order.Email,
                order.Phone,
                order.Product
            });

            RestResponse response = await Api.Client.ExecuteAsync(request);
            return response.IsSuccessful;
        }
        public async Task<string> CheckProductAvailability(ProductAvailabilityRequestDto productAvailability)
        {
            var request = new RestRequest("orders/checkOrders", Method.Post);
            request.AddJsonBody(new
            {
                productAvailability.StartDate,
                productAvailability.EndDate,
                productAvailability.Quantity,
                productAvailability.Id
            });

            RestResponse<BaseMessageDto> response = await Api.Client.ExecuteAsync<BaseMessageDto>(request);

            if (response.IsSuccessful)
            {
                return "";
            }

            return response.Data.Message;

        }

    }
}

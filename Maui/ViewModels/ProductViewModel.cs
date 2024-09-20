using Maui.Common;
using Maui.DTO;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.ViewModels
{
    public class ProductViewModel
    {
        //public MProp<int> Id { get; set; } = new MProp<int>();

        public MProp<SingleProductDto> Product { get; set; } = new MProp<SingleProductDto>();
        public MProp<DateTime> StartDate { get; set; } = new MProp<DateTime>();
        public MProp<DateTime> EndDate { get; set; } = new MProp<DateTime>();
        public MProp<int> Quantity { get; set; } = new MProp<int>();
        public MProp<bool> ButtonEnabled { get; set; } = new MProp<bool>();
        public MProp<string> OrderMessage { get; set; } = new MProp<string>();

        public ProductViewModel()
        {
            StartDate.Value = DateTime.Now;
            EndDate.Value = DateTime.Now.AddDays(1);
            Quantity.OnChange = Validate;
            OrderCommand = new Command(Order);
            ButtonEnabled.Value = true;
            OrderMessage.Value = "";
        }

        private void Validate()
        {
            if(Quantity.Value > Product.Value.TotalQuantity)
            {
                ButtonEnabled.Value = false;
                Quantity.Error = "Maximum quantity is: " + Product.Value.TotalQuantity;
            }
            else if(Quantity.Value <= 0)
            {
                ButtonEnabled.Value = false;
                Quantity.Error = "Quantity must be grather that 0";
            }
            else
            {
                ButtonEnabled.Value = true;
                Quantity.Error = null;

            }
        }

        public Command OrderCommand { get; }

        private void Order()
        {
            var user = SecureStorage.Default.GetUser();

            if(user.IsAdmin)
            {
                OrderMessage.Error = "Admin can not order product!";
                return;
            }
            var isValid = CheckProductAvailability();
            if (!isValid)
                return;
            //ButtonEnabled.Value = false;

            var startDate = StartDate.Value;
            var endDate = EndDate.Value;
            var quantity = Quantity.Value;
            var id = Product.Value.Id;
            var email = user.Email;
            var phone = user.Phone;

            var request = new RestRequest("orders", Method.Post);
            request.AddJsonBody(new 
            { 
                startDate, 
                endDate, 
                quantity, 
                email,
                phone,
                id, 
                product = Product.Value
            });

            RestResponse<TokenDto> response = Api.Client.Execute<TokenDto>(request);

            if (response.IsSuccessful)
            {
                OrderMessage.Value = "You have successfully ordered, please check your email!";
                OrderMessage.Error = null;
            }
            else
            {
                OrderMessage.Error = "Server error! Please try again";
            }
        }

        private bool CheckProductAvailability()
        {
            var startDate = StartDate.Value;
            var endDate = EndDate.Value;
            var quantity = Quantity.Value;
            var id = Product.Value.Id;

            var request = new RestRequest("orders/checkOrders", Method.Post);
            request.AddJsonBody(new { startDate, endDate, quantity, id });

            RestResponse<BaseMessageDto> response = Api.Client.Execute<BaseMessageDto>(request);

            if(response.IsSuccessful)
            {
                Quantity.Error = null;
                return true;
            }
                

            Quantity.Error = response.Data.Message;
            return false;
        }



        public void LoadProduct(int productId)
        {
            if (productId <= 0)
            {
                Product.Error = "Server error!";
                Product.Value = null;
                return;
            }

            RestRequest request = new RestRequest($"products/show/{productId}");
            var response = Api.Client.Execute<SingleProductDto>(request);
            if (response.IsSuccessful)
            {

                response.Data.Path = $"{Api.BaseImageUrl}{response.Data.Path}";
                Product.Value = response.Data;
                Product.Error = null;
            }
            else
            {
                Product.Error = "Server error!";
                Product.Value = null;

            }

        }
    }
}

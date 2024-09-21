using Maui.Common;
using Maui.DTO;
using Maui.Services.Interfaces;
using RestSharp;


namespace Maui.ViewModels
{
    public class ProductViewModel //: IProductService
    {
        //public MProp<int> Id { get; set; } = new MProp<int>();

        private readonly IProductService _productService;
        private readonly IOrderService _orderService;

        public MProp<SingleProductDto> Product { get; set; } = new MProp<SingleProductDto>();
        public MProp<DateTime> StartDate { get; set; } = new MProp<DateTime>();
        public MProp<DateTime> EndDate { get; set; } = new MProp<DateTime>();
        public MProp<int> Quantity { get; set; } = new MProp<int>();
        public MProp<bool> ButtonEnabled { get; set; } = new MProp<bool>();
        public MProp<string> OrderMessage { get; set; } = new MProp<string>();

        public ProductViewModel(IProductService productService, IOrderService orderService)
        {

            _productService = productService;

            InitializeViewModel();
            _orderService = orderService;
        }

        private void InitializeViewModel()
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

        public Command OrderCommand { get; set; }

        private async void Order()
        {
            var user = SecureStorage.Default.GetUser();
            if(user.IsAdmin)
            {
                OrderMessage.Error = "Admin can not order product!";
                return;
            }

            var isValid = await CheckProductAvailability();
            if (!isValid)
                return;

            ButtonEnabled.Value = false;

            var orderRequest = new OrderRequestDto
            {
                StartDate = StartDate.Value,
                EndDate = EndDate.Value,
                Quantity = Quantity.Value,
                Email = user.Email,
                Phone = user.Phone,
                Product = Product.Value
            };

            bool isOrdered = await _orderService.PlaceOrder(orderRequest);

            if (isOrdered)
            {
                OrderMessage.Value = "You have successfully ordered, please check your email!";
                OrderMessage.Error = null;
            }
            else
            {
                OrderMessage.Error = "Server error! Please try again.";
            }
        }

        private async Task<bool> CheckProductAvailability()
        {
            var availabilityRequest = new ProductAvailabilityRequestDto
            {
                StartDate = StartDate.Value,
                EndDate = EndDate.Value,
                Quantity = Quantity.Value,
                Id = Product.Value.Id
            };

            string responseMessgae =  await _orderService.CheckProductAvailability(availabilityRequest);
         

            if(string.IsNullOrEmpty(responseMessgae))
            {
                Quantity.Error = null;
                return true;
            }

            Quantity.Error = responseMessgae;
            return false;
        }

        public void LoadProduct(int productId)
        {

            var product = _productService.GetProduct(productId);
            if (product != null)
            {
                Product.Value = product;
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

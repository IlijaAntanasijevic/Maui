using FluentValidation;
using Maui.Common;
using RestSharp;

using System.Windows.Input;

namespace Maui.ViewModels
{
    public class AddProductViewModel : ProductFormViewModel
    {
        public MProp<bool> ShowPreviewImage { get; set; } = new MProp<bool>();

        public MProp<string> ErrorMessage { get; set; } = new MProp<string>();

        private FileResult photo;

        public AddProductViewModel()
            :base()
        {
           
            AddImageCommand = new Command(AddImage);
            AddProductCommand = new Command(AddProduct);
            ErrorMessage.Value = null;
        }

        public ICommand AddProductCommand { get; }

        public ICommand AddImageCommand { get; }

        private async void AddProduct()
        {
            string name = Name.Value;
            decimal price = Price.Value;
            int totalQuantity = TotalQuantity.Value;
            string details = Details.Value;

            var request = new RestRequest("products", Method.Post);

            request.AddParameter("name", name);
            request.AddParameter("price", price);
            request.AddParameter("totalQuantity", totalQuantity);
            request.AddParameter("details", details);

            var client = Api.Client;

            if (photo != null)
            {
                string mimeType = Utility.GetMimeType(photo.FileName);

                if(mimeType == null)
                {
                    FileSource.Error = "Only .jpg, .png, .jpeg";
                    return;
                }
                using (var stream = await photo.OpenReadAsync())
                {
                    byte[] imageBytes;
                    using (var memoryStream = new MemoryStream())
                    {
                        await stream.CopyToAsync(memoryStream);
                        imageBytes = memoryStream.ToArray();
                    }

                    request.AddFile("productImage", imageBytes, photo.FileName, mimeType);
                }
            }
            else
            {
                ErrorMessage.Value = "Image is invalid!";
            }

            RestResponse response = Api.Client.Execute(request);

            if (response.IsSuccessful)
            {
                await Application.Current.MainPage.Navigation.PopAsync();
                //App.Current.MainPage = new Admin();
            }
            else
            {
                ErrorMessage.Value = "Server error!";
            }
        }

        private async void AddImage()
        {
            photo = await MediaPicker.Default.PickPhotoAsync(new MediaPickerOptions());
            if (photo != null)
            {
                var stream = await photo.OpenReadAsync();
                FileSource.Value = ImageSource.FromStream(() => stream);
                ShowPreviewImage.Value = true;
            }
        }
    }
}

using Maui.Common;
using Maui.DTO;
using Maui.Validators;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.ViewModels
{
    public class ChangeEmailViewModel
    {
        public MProp<string> Email { get; set; } = new MProp<string>();
        public MProp<bool> ButtonEnabled { get; set; } = new MProp<bool>();
        public MProp<string> SuccessMessage { get; set; } = new MProp<string>();
        public ChangeEmailViewModel()
        {
            GetEmail();
            Email.OnChange = Validate;
            ButtonEnabled.Value = true;
            ChangeEmailCommand = new Command(ChangeEmail);
            CancelCommand = new Command(Cancel);
        }

        public Command ChangeEmailCommand { get; }
        public Command CancelCommand { get; }

        private void GetEmail()
        {
            RestRequest request = new RestRequest("admin/email");

            var response = Api.Client.Execute(request);
            var email = JsonConvert.DeserializeObject<EmailDto>(response.Content).Email;
            if (response.IsSuccessful && email != null)
            {
                Email.Value = email;
                Email.Error = null;
            }
            else
            {
                Email.Error = "Server error!";
            }
        } 

        private void Validate()
        {
            EmailValidator validator = new();
            var result = validator.Validate(this);

            if (!result.IsValid)
            {
                Email.Error = result.Errors.FirstOrDefault(x => x.PropertyName == "Email" + ".Value")?.ErrorMessage;
                ButtonEnabled.Value = false;
            }
            else
            {
                Email.Error = null;
                ButtonEnabled.Value = true;
            }
        }

        private void ChangeEmail()
        {
            if(String.IsNullOrEmpty(Email.Value))
            {
                Email.Error = "Email is not valid";
                return;
            }
            ButtonEnabled.Value = false;
            var email = Email.Value;

            RestRequest request = new RestRequest("admin/changeEmail", Method.Post);
            request.AddJsonBody(new { email });

            var response = Api.Client.Execute(request);

            if (response.IsSuccessful)
            {
                SuccessMessage.Value = "Successfully changed email address";
                Email.Error = null;
            }
            else
            {
                Email.Error = "Server error, please try again!";
                SuccessMessage.Value = null;
            }

        }

        private async void Cancel()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}

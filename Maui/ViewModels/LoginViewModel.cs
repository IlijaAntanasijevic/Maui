using Maui.Common;
using Maui.DTO;
using Maui.Pages;
using Maui.Validators;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Maui.ViewModels
{
    public class LoginViewModel
    {
        public MProp<string> Username { get; set; } = new MProp<string>();
        public MProp<string> Password { get; set; } = new MProp<string>();
        public MProp<bool> ButtonEnabled { get; set; } = new MProp<bool>();
        public MProp<bool> InvalidCredentials { get; set; } = new MProp<bool>();

        public MProp<bool> IsRegistration { get; set; } = new MProp<bool>();
        public MProp<bool> ShowRegistrationOption { get; set; } = new MProp<bool>();

        //private static bool isRegistartion { get; set; } = false;

        public LoginViewModel()
        {
            LoginCommand = new Command(Login);
            RegisterCommand = new Command(RegisterPage);
            ButtonEnabled.Value = false;
            IsRegistration.Value = Utility.IsRegistration;
            ShowRegistrationOption.Value = !Utility.IsRegistration;

            Username.OnChange = Validate;
            Password.OnChange = Validate;

            //Username.Value = "CPO AG";
            //Password.Value = "cpoag";
        }

        private void Validate()
        {
            LoginValidator validator = new();
            var result = validator.Validate(this);

            if (!result.IsValid)
            {
                Username.Error = result.Errors.FirstOrDefault(x => x.PropertyName == "Username" + ".Value")?.ErrorMessage;
                Password.Error = result.Errors.FirstOrDefault(x => x.PropertyName == "Password" + ".Value")?.ErrorMessage;
                ButtonEnabled.Value = false;
            }
            else
            {
                Username.Error = null;
                Password.Error = null;
                ButtonEnabled.Value = true;
            }

        }

        public ICommand LoginCommand { get; }

        public ICommand RegisterCommand { get; }

        private async void Login()
        {
            string username = Username.Value;
            string password = Password.Value;
            IsRegistration.Value = Utility.IsRegistration = false;

            var request = new RestRequest("admin/login", Method.Post);
            request.AddJsonBody(new { username, password });

            RestResponse<TokenDto> response = Api.Client.Execute<TokenDto>(request);

            if(response.IsSuccessful)
            {
                await SecureStorage.Default.SetAsync("token", response.Data.Token);

                InvalidCredentials.Value = false;

                App.Current.MainPage = new NavigationPage(new TabPage());
                //App.Current.MainPage = new TabPage();
            }
            else
            {
                InvalidCredentials.Value = true;
            }
        }

        private void RegisterPage()
        {
            App.Current.MainPage = new RegisterPage();
        }
    }
}

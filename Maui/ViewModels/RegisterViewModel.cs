
using System.Windows.Input;
using Maui.Common;
using Maui.DTO;
using Maui.Pages;
using Maui.Validators;
using RestSharp;

namespace Maui.ViewModels;

public class RegisterViewModel
{
    public MProp<string> Username { get; set; } = new MProp<string>();
    public MProp<string> Password { get; set; } = new MProp<string>();
    public MProp<string> Email { get; set; } = new MProp<string>();
    public MProp<string> Phone { get; set; } = new MProp<string>();
    public MProp<bool> ButtonEnabled { get; set; } = new MProp<bool>();
    public MProp<string> ServerError { get; set; } = new MProp<string>();
    //public MProp<string> ServerMessage { get; set; } = new MProp<string>();

    public RegisterViewModel()
    {
        RegisterCommand = new Command(Register);
        ButtonEnabled.Value = false;
        Username.OnChange = Validate;
        Password.OnChange = Validate;
        Email.OnChange = Validate;
        Phone.OnChange = Validate;

    }
    
    public ICommand RegisterCommand { get; }

    private void Validate()
    {
        var validator = new RegisterValidator();

        var result = validator.Validate(this);

        if (!result.IsValid)
        {
            Username.Error = result.Errors.FirstOrDefault(x => x.PropertyName == "Username" + ".Value")?.ErrorMessage;
            Password.Error = result.Errors.FirstOrDefault(x => x.PropertyName == "Password" + ".Value")?.ErrorMessage;
            Email.Error = result.Errors.FirstOrDefault(x => x.PropertyName == "Email" + ".Value")?.ErrorMessage;
            Phone.Error = result.Errors.FirstOrDefault(x => x.PropertyName == "Phone" + ".Value")?.ErrorMessage;
            
            ButtonEnabled.Value = false;
        }
        else
        {
            Username.Error = null;
            Password.Error = null;
            Email.Error = null;
            Phone.Error = null;
            ButtonEnabled.Value = true;
        }
    }

    private void Register()
    {
        var username = Username.Value;
        var email = Email.Value;
        var phone = Phone.Value;
        var password = Password.Value;

        RestRequest request = new RestRequest("admin/register", Method.Post);

        request.AddJsonBody(new { email, password, username, phone });

        RestResponse<BaseMessageDto> response = Api.Client.Execute<BaseMessageDto>(request);

        if (response.IsSuccessful)
        {
            ServerError.Error = null;
            Utility.IsRegistration = true;
            App.Current.MainPage = new LoginPage();
        }
        else
        {
            ServerError.Error = response.Data.Message;
            ServerError.Value = response.Data.Message;
        }
    }

}
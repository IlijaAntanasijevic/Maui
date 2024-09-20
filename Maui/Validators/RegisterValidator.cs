using FluentValidation;
using Maui.ViewModels;

namespace Maui.Validators;

public class RegisterValidator : AbstractValidator<RegisterViewModel>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Username.Value).NotEmpty()
                                    .WithMessage("Username is required");
        

        RuleFor(x => x.Password.Value).NotEmpty()
                                        .WithMessage("Password is required")
                                        .MinimumLength(8)
                                        .WithMessage("Minimum 8 characters");

        RuleFor(x => x.Email.Value).NotEmpty()
                                 .WithMessage("Email is required")
                                 .EmailAddress()
                                 .WithMessage("Please enter a valid email address");

        RuleFor(x => x.Phone.Value).NotEmpty()
                                 .WithMessage("Phone is required");
    }
}
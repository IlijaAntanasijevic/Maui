using FluentValidation;
using Maui.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.Validators
{
    public class LoginValidator : AbstractValidator<LoginViewModel>
    {
        public LoginValidator()
        {
            CascadeMode mode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Username.Value).NotEmpty()
                .WithMessage("Username is required");

            RuleFor(x => x.Password.Value).NotEmpty()
                                    .WithMessage("Password is required");
        }
    }

    public class EmailValidator : AbstractValidator<ChangeEmailViewModel>
    {
        public EmailValidator()
        {
            CascadeMode mode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Email.Value).NotEmpty()
                                       .WithMessage("Email must have some value")
                                       .EmailAddress()
                                       .WithMessage("Please enter a valid email address");
        }
    }
}

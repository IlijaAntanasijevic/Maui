using FluentValidation;
using Maui.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.Validators
{
    public class ProductFormValidator : AbstractValidator<ProductFormViewModel>
    {
        public ProductFormValidator()
        {
            CascadeMode mode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Name.Value).NotEmpty()
                                .WithMessage("Name is required")
                                .MinimumLength(3)
                                .WithMessage("Minimum 3 characters");

            RuleFor(x => x.Price.Value).NotEmpty()
                                 .WithMessage("Price is required")
                                 .GreaterThan(0)
                                 .WithMessage("Price must be greater than 0");


            RuleFor(x => x.TotalQuantity.Value).NotEmpty()
                                         .WithMessage("Total quantity is required")
                                         .GreaterThan(0)
                                         .WithMessage("Total quantity must be greater than 0");

            RuleFor(x => x.Details.Value).NotEmpty()
                                         .WithMessage("Details is required");

            RuleFor(x => x.FileSource.Value).NotEmpty()
                                            .WithMessage("Image is required");


        }
    }
}

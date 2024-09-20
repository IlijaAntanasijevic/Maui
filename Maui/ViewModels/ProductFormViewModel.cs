using Maui.Common;
using Maui.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.ViewModels
{
    public abstract class ProductFormViewModel 
    {
        public MProp<string> Name { get; set; } = new MProp<string>();
        public MProp<decimal> Price { get; set; } = new MProp<decimal>();
        public MProp<int> TotalQuantity { get; set; } = new MProp<int>();
        public MProp<string> Details { get; set; } = new MProp<string>();
        public MProp<bool> ButtonEnabled { get; set; } = new MProp<bool>();
        public MProp<ImageSource> FileSource { get; set; } = new MProp<ImageSource>();

        protected ProductFormViewModel()
        {
            Name.OnChange = Validate;
            Price.OnChange = Validate;
            TotalQuantity.OnChange = Validate;
            Details.OnChange = Validate;
            FileSource.OnChange = Validate;
        }



        private void Validate()
        {
            ProductFormValidator validator = new();
            var result = validator.Validate(this);

            if (!result.IsValid)
            {
                Name.Error = result.Errors.FirstOrDefault(x => x.PropertyName == "Name" + ".Value")?.ErrorMessage;
                Price.Error = result.Errors.FirstOrDefault(x => x.PropertyName == "Price" + ".Value")?.ErrorMessage;
                TotalQuantity.Error = result.Errors.FirstOrDefault(x => x.PropertyName == "TotalQuantity" + ".Value")?.ErrorMessage;
                Details.Error = result.Errors.FirstOrDefault(x => x.PropertyName == "Details" + ".Value")?.ErrorMessage;
                FileSource.Error = result.Errors.FirstOrDefault(x => x.PropertyName == "FileSource" + ".Value")?.ErrorMessage;

            }
            else
            {
                Name.Error = null;
                Price.Error = null;
                TotalQuantity.Error = null;
                Details.Error = null;
                FileSource.Error = null;
                ButtonEnabled.Value = true;
            }

        }
    }
}

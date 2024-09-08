using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelView
{
    public class MultiLineAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if ((value as string).Length < 100)
            {
                return new ValidationResult("The Dicription Length Is Very Short");
            }
            return ValidationResult.Success;
        }
    }
}

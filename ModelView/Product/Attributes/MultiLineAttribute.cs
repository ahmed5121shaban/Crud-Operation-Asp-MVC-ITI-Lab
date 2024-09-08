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
            //string discription = value as string;
            //if (!(discription.Contains("\n")) && (discription.Length<200))
            //{
            //    return new ValidationResult("The Dicription Is Not Enough");
            //}
            return ValidationResult.Success;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace VintriCodingAssignment.DTOs
{
    public class ValidateEmailAddress : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string Pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
            var regex = new Regex(Pattern, RegexOptions.IgnoreCase);
            var review = (ReviewDTO)validationContext.ObjectInstance;

            if (review.Username == null)
                return new ValidationResult("Username must be valid email address, it should not be null");

            if(!regex.IsMatch(review.Username))
            {
                return new ValidationResult("Please enter valid email address");
            } else
            {
                return ValidationResult.Success;
            }

        }
    }
}
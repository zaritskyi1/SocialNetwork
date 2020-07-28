using System;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.BLL.Validators
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class MinimumAgeAttribute : ValidationAttribute
    {
        private readonly int _minimumAge;

        public MinimumAgeAttribute(int minimumAge)
        {
            _minimumAge = minimumAge;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult($"{validationContext.DisplayName} is required field.");
            }

            if (!DateTime.TryParse(value.ToString(), out var date))
            {
                return new ValidationResult("Invalid date format.");
            }

            var result = date.AddYears(_minimumAge) < DateTime.UtcNow;
            return result? ValidationResult.Success : new ValidationResult("Age must be greater than 18.");
        }
    }
}

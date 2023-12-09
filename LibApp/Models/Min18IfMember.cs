using System.ComponentModel.DataAnnotations;

namespace LibApp.Models
{
    public class Min18IfMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer)validationContext.ObjectInstance;
            if (customer.MembershipTypeId == 1)
            {
                return ValidationResult.Success;
            }

            if (customer.Birthdate == null) 
            {
                return new ValidationResult("Birthdate is required");
            }

            var age = DateTime.Today.Year - customer.Birthdate.Value.Year;

            return age >= 18 ? ValidationResult.Success : new ValidationResult("Has to be 18 years old to subscribe");
        }
    }
}

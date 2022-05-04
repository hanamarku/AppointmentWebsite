using System;
using System.ComponentModel.DataAnnotations;

namespace Online_Appointment.ValidationHelpers
{
    public class BirthdayValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime date;
            bool parsed = DateTime.TryParse(value.ToString(), out date);
            if (!parsed)
                return new ValidationResult("Invalid Date");
            else
            {
                var min = DateTime.Now.AddYears(-15);
                var max = DateTime.Now.AddYears(-150);
                var msg = string.Format("Please enter a value between {0:dd/MM/yyyy} and {1:dd/MM/yyyy}", max, min);
                try
                {
                    if (date > min || date < max)
                        return new ValidationResult(msg);
                    else
                        return ValidationResult.Success;
                }
                catch (Exception e)
                {
                    return new ValidationResult(e.Message);
                }
            }
        }
    }

}

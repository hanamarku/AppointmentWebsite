using System;
using System.ComponentModel.DataAnnotations;

namespace Online_Appointment.ValidationHelpers
{
    public class AppointmentDateValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime date;
            bool parsed = DateTime.TryParse(value.ToString(), out date);
            if (!parsed)
                return new ValidationResult("Invalid Date");
            else
            {
                var currentDate = DateTime.Now.Date;
                var msg = "Date selected  must be on or after today  ";
                try
                {
                    if (date.Date <= currentDate)
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

using System;
using System.ComponentModel.DataAnnotations;

namespace Online_Appointment.ValidationHelpers
{
    public class Time1 : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime time1;
            bool parsed = DateTime.TryParse(value.ToString(), out time1);
            if (!parsed)
                return new ValidationResult("Invalid time");
            else
            {
                var time = DateTime.Now.ToLocalTime();

                var msg = "Doctor not available";
                try
                {
                    if (time < time1)
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

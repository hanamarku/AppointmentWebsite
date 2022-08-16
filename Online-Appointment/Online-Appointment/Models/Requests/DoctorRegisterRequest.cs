using Online_Appointment.ValidationHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Online_Appointment.Models.Requests
{

    public class DoctorRegisterRequest
    {
        [Required(ErrorMessage = "First Name is required !")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Invalid First Name")]
        [Display(Name = "Firstname")]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "Last Name is required !")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Invalid Last Name")]
        [Display(Name = "Lastname")]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Username is required !")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Birthday is required !")]
        [DataType(DataType.Date)]
        [BirthdayValidation]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Birthday { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        [Required(ErrorMessage = "Phone number is required !")]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.MultilineText)]

        public string Specialization { get; set; }
        [DataType(DataType.Time)]
        [Display(Name = "Start time")]
        public DateTime StartTime { get; set; }
        [DataType(DataType.Time)]
        [Display(Name = "End time")]
        public DateTime EndTime { get; set; }
        public Status Status { get; set; }
        public int Departament { get; set; }

        [Display(Name = "UserPhoto")]
        public byte[] UserPhoto { get; set; }
        //[Display(Name = "Image")]
        //public string ImageURL { get; set; }
        //public HttpPostedFileBase File { get; set; }
    }
}
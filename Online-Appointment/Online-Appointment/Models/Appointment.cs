using Online_Appointment.ValidationHelpers;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Appointment.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }

        public string PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual ApplicationUser User { get; set; }


        public string DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public virtual Doctor Doctor { get; set; }

        [Required(ErrorMessage = "Date is required")]

        [DisplayName("Appointment Date")]
        [DataType(DataType.Date)]
        [AppointmentDateValidation]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]

        //    [Remote("ValidateDateEqualOrGreater", HttpMethod = "Post",
        //ErrorMessage = "Date isn't equal or greater than current date.")]
        public DateTime? AppointmentDate { get; set; }

        [Required(ErrorMessage = "Time is required")]
        [DisplayName("Appointment Time")]
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }
        [Required(ErrorMessage = "Date is required")]
        public string Description { get; set; }

        public bool Status { get; set; }
        public string CreatorId { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedById { get; set; }
        public DateTime DeletedOn { get; set; }
        public string DeletedById { get; set; }
    }
}

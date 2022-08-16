using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Appointment.Models
{
    public class Prescription
    {
        [Key]
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        [ForeignKey("AppointmentId")]
        public Appointment Appointement { get; set; }
        //public virtual List<Medicine> Medicines { get; set; }
        public DateTime PrescriptionDate { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Decription is required")]
        [Display(Name = "The name of the medication, the dose, and the number of times a day you have to take it.")]
        public string Description { get; set; }
    }
}
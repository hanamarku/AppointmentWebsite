using System;
using System.Collections.Generic;
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
        public Appointement Appointement { get; set; }
        public virtual List<Medicine> Medicines { get; set; }
        public DateTime PrescriptionDate { get; set; }
    }
}
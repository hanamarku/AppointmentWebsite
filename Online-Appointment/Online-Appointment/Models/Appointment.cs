using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Online_Appointment.Models.Users;

namespace Online_Appointment.Models
{
    public class Appointement
    {
        [Key]
        public int Id { get; set; }
        [Required]

        public int UserId { get; set; }
        [ForeignKey("Id")]
        public virtual User User { get; set; }


        [DisplayName("Appointment Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? AppointmentDate { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool Status { get; set; }
        public int CreatorId { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int UpdatedById { get; set; }
        public DateTime DeletedOn { get; set; }
        public int? DeletedById { get; set; }
    }
}

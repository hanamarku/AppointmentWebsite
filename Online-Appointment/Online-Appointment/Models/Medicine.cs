using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Online_Appointment.Models
{
    public class Medicine
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required !")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required !")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Quantity is required !")]
        [Range(0, 100, ErrorMessage = "Quantity must be between 0-100")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Price is required !")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Plotesoni kohen !")]
        [DisplayName("Time to take medicine")]
        public string TimeToTakeMedicine { get; set; }
        public virtual List<Prescription> PrescriptionList { get; set; }
    }
}
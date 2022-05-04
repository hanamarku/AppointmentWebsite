using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using System.Web.Mvc;


namespace Online_Appointment.Models
{
    public class Department
    {
        [Key]
        public int DepId { get; set; }
        [Required(ErrorMessage = "Name is required !")]
        [Remote("NameExists", "Admin", ErrorMessage = "Name already exists !")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required !")]
        public string Description { get; set; }
        [Display(Name = "Image")]
        public string ImageURL { get; set; }
        [NotMapped]
        public HttpPostedFileBase File { get; set; }
        public virtual List<Doctor> Doctors { get; set; }
    }
}
using Online_Appointment.Models;
using Online_Appointment.Models.Requests;
using System.Collections.Generic;

namespace Online_Appointment.CollectionOfModels
{
    public class DoctorCollection
    {
        public DoctorRegisterRequest DoctorRegisterRequest { get; set; }
        public IEnumerable<Department> Departments { get; set; }
        public Doctor Doctor { get; set; }
    }
}
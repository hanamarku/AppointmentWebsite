using Online_Appointment.Models;
using System.Collections.Generic;

namespace Online_Appointment.CollectionOfModels
{
    public class IndexCollection
    {
        public IEnumerable<Department> Departments { get; set; }
        public IEnumerable<Doctor> Doctors { get; set; }
    }
}
using Online_Appointment.Models;
using System.Collections.Generic;

namespace Online_Appointment.CollectionOfModels
{
    public class DashboardCollection
    {
        public IEnumerable<Department> Departments { get; set; }
        public IEnumerable<Users> Users { get; set; }
    }
}
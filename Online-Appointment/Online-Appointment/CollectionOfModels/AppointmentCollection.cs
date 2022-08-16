
using Online_Appointment.Models;
using System.Collections.Generic;

namespace Online_Appointment.CollectionOfModels
{
    public class AppointmentCollection
    {
        public Appointment Appointment { get; set; }
        public IEnumerable<Doctor> Doctor { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }

    }
}
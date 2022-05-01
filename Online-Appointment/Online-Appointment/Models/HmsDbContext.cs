using System.Data.Entity;
using static Online_Appointment.Models.Users;

namespace Online_Appointment.Models
{
    public class HmsDbContext : DbContext
    {

        public DbSet<Appointement> Appointements { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<User> Users { get; set; }
        public HmsDbContext() : base("hmsAppointmentDb")
        {

        }

        protected HmsDbContext(string connString) : base(connString)
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelbuilder)
        {
            //modelbuilder.Entity<Doctor>()
            //    .HasKey(x => x.DoctorId);
            //   modelbuilder.Entity<Doctor>().HasOptional(t => t.Departament)
            //.WithMany()
            //.Map(d => d.MapKey("DepId"));
        }


    }
}
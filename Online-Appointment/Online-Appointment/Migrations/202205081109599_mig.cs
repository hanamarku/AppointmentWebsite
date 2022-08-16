namespace Online_Appointment.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class mig : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Appointments", "AppointmentDate", c => c.DateTime(nullable: false));
        }

        public override void Down()
        {
            AlterColumn("dbo.Appointments", "AppointmentDate", c => c.DateTime());
        }
    }
}

namespace Online_Appointment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appName : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Appointements", newName: "Appointments");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Appointments", newName: "Appointements");
        }
    }
}

namespace Online_Appointment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Appointments", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Appointments", "PatientId");
            RenameColumn(table: "dbo.Appointments", name: "ApplicationUser_Id", newName: "PatientId");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.Appointments", name: "PatientId", newName: "ApplicationUser_Id");
            AddColumn("dbo.Appointments", "PatientId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Appointments", "ApplicationUser_Id");
        }
    }
}

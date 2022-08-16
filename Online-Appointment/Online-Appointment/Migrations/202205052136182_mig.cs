namespace Online_Appointment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Appointments", "PatientId", "dbo.AspNetUsers");
            DropIndex("dbo.Appointments", new[] { "PatientId" });
            AddColumn("dbo.Appointments", "User_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Appointments", "PatientId", c => c.String());
            CreateIndex("dbo.Appointments", "User_Id");
            AddForeignKey("dbo.Appointments", "User_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Appointments", new[] { "User_Id" });
            AlterColumn("dbo.Appointments", "PatientId", c => c.String(maxLength: 128));
            DropColumn("dbo.Appointments", "User_Id");
            CreateIndex("dbo.Appointments", "PatientId");
            AddForeignKey("dbo.Appointments", "PatientId", "dbo.AspNetUsers", "Id");
        }
    }
}

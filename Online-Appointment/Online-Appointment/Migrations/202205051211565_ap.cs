namespace Online_Appointment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ap : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Appointements", name: "UserId", newName: "PatientId");
            RenameIndex(table: "dbo.Appointements", name: "IX_UserId", newName: "IX_PatientId");
            AddColumn("dbo.Appointements", "DoctorId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Appointements", "DoctorId");
            AddForeignKey("dbo.Appointements", "DoctorId", "dbo.Doctors", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointements", "DoctorId", "dbo.Doctors");
            DropIndex("dbo.Appointements", new[] { "DoctorId" });
            DropColumn("dbo.Appointements", "DoctorId");
            RenameIndex(table: "dbo.Appointements", name: "IX_PatientId", newName: "IX_UserId");
            RenameColumn(table: "dbo.Appointements", name: "PatientId", newName: "UserId");
        }
    }
}

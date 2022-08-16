namespace Online_Appointment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class miayg : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Appointments", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Appointments", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Appointments", "ApplicationUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Appointments", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Appointments", "ApplicationUser_Id");
            AddForeignKey("dbo.Appointments", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}

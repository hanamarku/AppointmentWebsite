namespace Online_Appointment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class med2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Prescriptions", "Medicine_Id", "dbo.Medicines");
            DropIndex("dbo.Prescriptions", new[] { "Medicine_Id" });
            DropColumn("dbo.Prescriptions", "Medicine_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Prescriptions", "Medicine_Id", c => c.Int());
            CreateIndex("dbo.Prescriptions", "Medicine_Id");
            AddForeignKey("dbo.Prescriptions", "Medicine_Id", "dbo.Medicines", "Id");
        }
    }
}

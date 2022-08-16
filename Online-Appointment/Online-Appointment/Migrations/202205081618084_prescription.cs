namespace Online_Appointment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class prescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Prescriptions", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Prescriptions", "Description");
        }
    }
}

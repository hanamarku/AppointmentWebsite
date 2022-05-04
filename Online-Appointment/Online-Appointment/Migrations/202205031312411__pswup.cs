namespace Online_Appointment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _pswup : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Password", c => c.String());
        }
    }
}

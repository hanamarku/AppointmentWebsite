namespace Online_Appointment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user_photo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctors", "UserPhoto", c => c.Binary());
            DropColumn("dbo.Doctors", "ImageURL");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Doctors", "ImageURL", c => c.String());
            DropColumn("dbo.Doctors", "UserPhoto");
        }
    }
}

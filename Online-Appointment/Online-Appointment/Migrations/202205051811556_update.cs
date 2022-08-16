namespace Online_Appointment.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class update : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Appointments", "CreatorId", c => c.String());
        }

        public override void Down()
        {
            AlterColumn("dbo.Appointments", "CreatorId", c => c.Int(nullable: true));
        }
    }
}

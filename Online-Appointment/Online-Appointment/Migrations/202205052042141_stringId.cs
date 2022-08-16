namespace Online_Appointment.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class stringId : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Appointments", "UpdatedById", c => c.String());
            AlterColumn("dbo.Appointments", "DeletedById", c => c.String());
        }

        public override void Down()
        {
            AlterColumn("dbo.Appointments", "DeletedById", c => c.Int());
            AlterColumn("dbo.Appointments", "UpdatedById", c => c.Int(nullable: true));
        }
    }
}

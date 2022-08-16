namespace Online_Appointment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class medicine : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MedicinePrescriptions", "Medicine_Id", "dbo.Medicines");
            DropForeignKey("dbo.MedicinePrescriptions", "Prescription_Id", "dbo.Prescriptions");
            DropIndex("dbo.MedicinePrescriptions", new[] { "Medicine_Id" });
            DropIndex("dbo.MedicinePrescriptions", new[] { "Prescription_Id" });
            AddColumn("dbo.Prescriptions", "Medicine_Id", c => c.Int());
            CreateIndex("dbo.Prescriptions", "Medicine_Id");
            AddForeignKey("dbo.Prescriptions", "Medicine_Id", "dbo.Medicines", "Id");
            DropTable("dbo.MedicinePrescriptions");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MedicinePrescriptions",
                c => new
                    {
                        Medicine_Id = c.Int(nullable: false),
                        Prescription_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Medicine_Id, t.Prescription_Id });
            
            DropForeignKey("dbo.Prescriptions", "Medicine_Id", "dbo.Medicines");
            DropIndex("dbo.Prescriptions", new[] { "Medicine_Id" });
            DropColumn("dbo.Prescriptions", "Medicine_Id");
            CreateIndex("dbo.MedicinePrescriptions", "Prescription_Id");
            CreateIndex("dbo.MedicinePrescriptions", "Medicine_Id");
            AddForeignKey("dbo.MedicinePrescriptions", "Prescription_Id", "dbo.Prescriptions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MedicinePrescriptions", "Medicine_Id", "dbo.Medicines", "Id", cascadeDelete: true);
        }
    }
}

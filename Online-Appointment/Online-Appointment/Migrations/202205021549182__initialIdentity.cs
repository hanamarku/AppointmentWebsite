namespace Online_Appointment.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class _initialIdentity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointements",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.String(maxLength: 128),
                    AppointmentDate = c.DateTime(),
                    Description = c.String(nullable: false),
                    Status = c.Boolean(nullable: false),
                    CreatorId = c.Int(nullable: true),
                    UpdatedOn = c.DateTime(nullable: true),
                    UpdatedById = c.Int(nullable: true),
                    DeletedOn = c.DateTime(nullable: true),
                    DeletedById = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.AspNetUsers",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Firstname = c.String(),
                    Lastname = c.String(),
                    Password = c.String(),
                    Birthday = c.DateTime(nullable: false),
                    CreatedOn = c.DateTime(nullable: false),
                    Email = c.String(maxLength: 256),
                    EmailConfirmed = c.Boolean(nullable: false),
                    PasswordHash = c.String(),
                    SecurityStamp = c.String(),
                    PhoneNumber = c.String(),
                    PhoneNumberConfirmed = c.Boolean(nullable: false),
                    TwoFactorEnabled = c.Boolean(nullable: false),
                    LockoutEndDateUtc = c.DateTime(),
                    LockoutEnabled = c.Boolean(nullable: false),
                    AccessFailedCount = c.Int(nullable: false),
                    UserName = c.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");

            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.String(nullable: false, maxLength: 128),
                    ClaimType = c.String(),
                    ClaimValue = c.String(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                {
                    LoginProvider = c.String(nullable: false, maxLength: 128),
                    ProviderKey = c.String(nullable: false, maxLength: 128),
                    UserId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                {
                    UserId = c.String(nullable: false, maxLength: 128),
                    RoleId = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);

            CreateTable(
                "dbo.Departments",
                c => new
                {
                    DepId = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false),
                    Description = c.String(nullable: false),
                    ImageURL = c.String(),
                })
                .PrimaryKey(t => t.DepId);

            CreateTable(
                "dbo.Doctors",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Specialization = c.String(),
                    StartTime = c.DateTime(nullable: false),
                    EndTime = c.DateTime(nullable: false),
                    Status = c.Int(nullable: false),
                    ImageURL = c.String(),
                    DepId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.DepId);

            CreateTable(
                "dbo.Prescriptions",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    AppointmentId = c.Int(nullable: false),
                    PrescriptionDate = c.DateTime(nullable: false),
                    Doctor_Id = c.String(maxLength: 128),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Appointements", t => t.AppointmentId, cascadeDelete: true)
                .ForeignKey("dbo.Doctors", t => t.Doctor_Id)
                .Index(t => t.AppointmentId)
                .Index(t => t.Doctor_Id);

            CreateTable(
                "dbo.Medicines",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false),
                    Description = c.String(nullable: false),
                    Quantity = c.Int(nullable: false),
                    Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    TimeToTakeMedicine = c.String(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.AspNetRoles",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Name = c.String(nullable: false, maxLength: 256),
                    Discriminator = c.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");

            CreateTable(
                "dbo.MedicinePrescriptions",
                c => new
                {
                    Medicine_Id = c.Int(nullable: false),
                    Prescription_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.Medicine_Id, t.Prescription_Id })
                .ForeignKey("dbo.Medicines", t => t.Medicine_Id, cascadeDelete: true)
                .ForeignKey("dbo.Prescriptions", t => t.Prescription_Id, cascadeDelete: true)
                .Index(t => t.Medicine_Id)
                .Index(t => t.Prescription_Id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Prescriptions", "Doctor_Id", "dbo.Doctors");
            DropForeignKey("dbo.MedicinePrescriptions", "Prescription_Id", "dbo.Prescriptions");
            DropForeignKey("dbo.MedicinePrescriptions", "Medicine_Id", "dbo.Medicines");
            DropForeignKey("dbo.Prescriptions", "AppointmentId", "dbo.Appointements");
            DropForeignKey("dbo.Doctors", "DepId", "dbo.Departments");
            DropForeignKey("dbo.Doctors", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Appointements", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.MedicinePrescriptions", new[] { "Prescription_Id" });
            DropIndex("dbo.MedicinePrescriptions", new[] { "Medicine_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Prescriptions", new[] { "Doctor_Id" });
            DropIndex("dbo.Prescriptions", new[] { "AppointmentId" });
            DropIndex("dbo.Doctors", new[] { "DepId" });
            DropIndex("dbo.Doctors", new[] { "Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Appointements", new[] { "UserId" });
            DropTable("dbo.MedicinePrescriptions");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Medicines");
            DropTable("dbo.Prescriptions");
            DropTable("dbo.Doctors");
            DropTable("dbo.Departments");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Appointements");
        }
    }
}

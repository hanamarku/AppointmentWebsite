using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Online_Appointment.Models;
using Owin;

[assembly: OwinStartupAttribute(typeof(Online_Appointment.Startup))]
namespace Online_Appointment
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }

        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                var user = new ApplicationUser();
                //user.Firstname = "adminName";
                //user.Lastname = "adminLastNAme";
                //user.Birthday = System.DateTime.Now;
                user.UserName = "admin";
                user.Email = "admin@gmail.com";
                //user.PhoneNumber = "0694298902";
                string PWD = "Admin123.";
                //user.CreatedOn = System.DateTime.Now;
                var adminUser = UserManager.Create(user, PWD);
                if (adminUser.Succeeded)
                {
                    UserManager.AddToRole(user.Id, "Admin");
                }
            }

            if (!roleManager.RoleExists("Doctor"))
            {
                var role = new IdentityRole();
                role.Name = "Doctor";
                roleManager.Create(role);

            }

            if (!roleManager.RoleExists("Patient"))
            {
                var role = new IdentityRole();
                role.Name = "Patient";
                roleManager.Create(role);

            }
        }
    }
}

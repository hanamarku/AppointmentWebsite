using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Online_Appointment.Startup))]
namespace Online_Appointment
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

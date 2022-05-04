using Online_Appointment.Models;
using System.Linq;
using System.Web.Mvc;

namespace Online_Appointment.Controllers
{
    public class HomeController : Controller
    {
        public ApplicationDbContext context = new ApplicationDbContext();
        public ActionResult Index()
        {
            ViewBag.departments = context.Departments.ToList();
            return View();
        }
        public ActionResult HomeIndex()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}